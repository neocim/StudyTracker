using Entity = Domain.Entities;
using Application.Data;
using Domain.Readers;
using MediatR;
using ErrorOr;

namespace Application.Cqrs.Commands.Task;

public record AddSubTaskCommand(
    Guid Id,
    Guid ParentTaskId,
    Guid OwnerId,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success)
    : IRequest<ErrorOr<Created>>;

public class AddSubTaskCommandHandler(IDataContext dataContext, ITaskReader taskReader)
    : IRequestHandler<AddSubTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(AddSubTaskCommand request,
        CancellationToken cancellationToken)
    {
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