using Application.Data;
using Domain.Readers;
using ErrorOr;
using MediatR;
using Entity = Domain.Entities;

namespace Application.Cqrs.Commands.Task;

public record CreateTaskCommand(
    Guid Id,
    Guid OwnerId,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success)
    : IRequest<ErrorOr<Created>>;

public class CreateTaskCommandHandler(IDataContext dataContext, ITaskReader taskReader)
    : IRequestHandler<CreateTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        if (await taskReader.GetByIdAsync(request.Id) is not null)
            return Error.Conflict(
                description: $"Task with ID `{request.Id}` is already exists");

        await dataContext.TaskRepository.Add(new Entity.Task(request.Id, request.OwnerId,
            request.BeginDate, request.EndDate, request.Name, request.Description,
            request.Success));
        await dataContext.SaveChangesAsync();

        return Result.Created;
    }
}