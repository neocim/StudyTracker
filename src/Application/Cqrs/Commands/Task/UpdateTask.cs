using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Cqrs.Commands.Task;

public record UpdateTaskCommand(
    Guid TaskId,
    bool? Success,
    string? Name,
    string? Description)
    : IRequest<ErrorOr<Success>>;

public class UpdateTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<UpdateTaskCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        task.Success = request.Success ?? task.Success;
        task.Name = request.Name ?? task.Name;
        task.Description = request.Description ?? task.Description;

        await taskRepository.UpdateAsync(task);

        return Result.Success;
    }
}