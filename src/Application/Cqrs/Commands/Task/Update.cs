using Application.Data;
using Application.Security;
using Application.Security.Permissions;
using Domain.Readers;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Commands.Task;

public record UpdateTaskCommand(
    Guid Id,
    string? Name,
    string? Description,
    bool? Success)
    : IRequest<ErrorOr<Updated>>;

public class UpdateTaskCommandHandler(
    IDataContext dataContext,
    ITaskReader taskReader,
    ISecurityContext securityContext,
    ILogger logger)
    : IRequestHandler<UpdateTaskCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        if (!securityContext.HasPermission(Permission.Task.Update))
            return Error.Forbidden(description: "Access denied");

        var task = await taskReader.GetByIdAsync(request.Id);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.Id}` doesn't exist");

        task.Success = request.Success ?? task.Success;
        task.Name = request.Name ?? task.Name;
        task.Description = request.Description ?? task.Description;

        await dataContext.TaskRepository.Update(task);
        await dataContext.SaveChangesAsync();

        logger.LogInformation($"Task `{request.Id}` was updated");

        return Result.Updated;
    }
}