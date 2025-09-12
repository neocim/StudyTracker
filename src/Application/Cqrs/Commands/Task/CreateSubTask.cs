using Entity = Domain.Entities;
using Application.Data;
using Application.Security;
using Application.Security.Permissions;
using Domain.Readers;
using MediatR;
using ErrorOr;

namespace Application.Cqrs.Commands.Task;

public record CreateSubTaskCommand(
    Guid Id,
    Guid ParentTaskId,
    Guid OwnerId,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success)
    : IRequest<ErrorOr<Created>>;

public class CreateSubTaskCommandHandler(
    IDataContext dataContext,
    ITaskReader taskReader,
    ISecurityContext securityContext)
    : IRequestHandler<CreateSubTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateSubTaskCommand request,
        CancellationToken cancellationToken)
    {
        if (!securityContext.HasPermission(Permission.Task.Create))
            return Error.Forbidden(description: "Access denied");

        var task = await taskReader.GetByIdAsync(request.ParentTaskId);

        if (task is null)
            return Error.NotFound(
                description:
                $"Task with ID `{request.ParentTaskId}` doesn't exist");

        task.AddSubTask(new Entity.Task(request.Id, request.OwnerId,
            request.BeginDate, request.EndDate, request.Name, request.Description,
            request.Success));
        await dataContext.TaskRepository.Update(task);
        await dataContext.SaveChangesAsync();

        return Result.Created;
    }
}