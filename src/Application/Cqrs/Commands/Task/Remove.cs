using Application.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Cqrs.Commands.Task;

public record RemoveTaskCommand(Guid TaskId) : IRequest<ErrorOr<Deleted>>;

public class RemoveTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<RemoveTaskCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(RemoveTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        await taskRepository.RemoveAsync(task);

        return Result.Deleted;
    }
}