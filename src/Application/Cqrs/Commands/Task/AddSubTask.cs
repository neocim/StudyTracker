using Application.Dto.Task;
using Domain.Repositories;
using MediatR;
using ErrorOr;

namespace Application.Cqrs.Commands.Task;

public record AddSubTaskCommand(SubTask SubTask)
    : IRequest<ErrorOr<Created>>;

public class AddSubTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<AddSubTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(AddSubTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.SubTask.ParentTaskId);

        if (task is null)
            return Error.NotFound(
                description:
                $"Task with ID `{request.SubTask.ParentTaskId}` doesn't exist");

        task.AddSubTask(request.SubTask.ToTaskEntity());
        await taskRepository.UpdateAsync(task);

        return Result.Created;
    }
}