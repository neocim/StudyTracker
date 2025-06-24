using Application.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Cqrs.Commands.Task;

public record UpdateTaskStatusCommand(Guid TaskId, bool Success)
    : IRequest<ErrorOr<Success>>;

public class UpdateTaskStatusCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<UpdateTaskStatusCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateTaskStatusCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        task.Success = request.Success;
        await taskRepository.UpdateAsync(task);

        return Result.Success;
    }
}