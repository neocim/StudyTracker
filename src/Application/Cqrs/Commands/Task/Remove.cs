using Application.Data;
using Domain.Readers;
using ErrorOr;
using MediatR;

namespace Application.Cqrs.Commands.Task;

public record RemoveTaskCommand(Guid TaskId) : IRequest<ErrorOr<Deleted>>;

public class RemoveTaskCommandHandler(IDataContext dataContext, ITaskReader taskReader)
    : IRequestHandler<RemoveTaskCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(RemoveTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskReader.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        await dataContext.TaskRepository.Remove(task);
        await dataContext.SaveChangesAsync();

        return Result.Deleted;
    }
}