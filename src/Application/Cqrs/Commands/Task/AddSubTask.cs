using Application.Repositories;
using MediatR;
using ErrorOr;
using Entity = Domain.Entities;

namespace Application.Cqrs.Commands.Task;

public record AddSubTaskCommand(Guid TaskId, Entity.Task SubTask)
    : IRequest<ErrorOr<Success>>;

public class AddSubTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<AddSubTaskCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddSubTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound($"Task with ID `{request.TaskId}` doesn't exist");

        task.AddSubTask(request.SubTask);
        await taskRepository.UpdateAsync(task);

        return Result.Success;
    }
}