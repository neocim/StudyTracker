using Application.Data;
using Application.Security;
using Application.Security.Permissions;
using Domain.Readers;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Commands.Task;

public record RemoveTaskCommand(Guid TaskId) : IRequest<ErrorOr<Deleted>>;

public class RemoveTaskCommandHandler(
    IDataContext dataContext,
    ITaskReader taskReader,
    ISecurityContext securityContext,
    ILogger logger)
    : IRequestHandler<RemoveTaskCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(RemoveTaskCommand request,
        CancellationToken cancellationToken)
    {
        if (!securityContext.HasPermission(Permission.Task.Delete))
            return Error.Forbidden(description: "Access denied");

        var task = await taskReader.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        await dataContext.TaskRepository.Remove(task);
        await dataContext.SaveChangesAsync();

        logger.LogInformation($"Task `{request.TaskId}` was removed");

        return Result.Deleted;
    }
}