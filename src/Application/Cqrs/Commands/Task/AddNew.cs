using Application.Repositories;
using ErrorOr;
using MediatR;
using Entity = Domain.Entities;

namespace Application.Cqrs.Commands.Task;

public record AddNewTaskCommand(Entity.Task Task)
    : IRequest<ErrorOr<Created>>;

public class AddNewTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<AddNewTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(AddNewTaskCommand request,
        CancellationToken cancellationToken)
    {
        if (await taskRepository.GetByIdAsync(request.Task.Id) is not null)
            return Error.Conflict(
                description: $"Task with ID `{request.Task.Id}` is already exists");

        await taskRepository.AddAsync(request.Task);

        return Result.Created;
    }
}