using Application.Dto.Task;
using Application.Repositories;
using MediatR;
using ErrorOr;
using Entity = Domain.Entities;

namespace Application.Cqrs.Commands.Task;

public record AddSubTaskCommand(Guid ParentTaskId, SubTask SubTask)
    : IRequest<ErrorOr<Created>>;

public class AddSubTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<AddSubTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(AddSubTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.ParentTaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.ParentTaskId}` doesn't exist");

        task.AddSubTask(request.SubTask.ToTaskEntity(task.OwnerId));
        await taskRepository.UpdateAsync(task);

        return Result.Created;
    }
}