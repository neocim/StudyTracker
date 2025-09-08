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
        try
        {
            await taskRepository.AddAsync(request.Task);
        }
        catch (DbUpdateExpection ex)
        {
        }

        return Result.Created;
    }
}