using Application.Repositories;
using MediatR;
using ErrorOr;
using Entity = Domain.Entities;

namespace Application.Cqrs.Queries.Task;

public record GetTaskByIdQuery(Guid TaskId) : IRequest<ErrorOr<Entity.Task>>;

public class GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetTaskByIdQuery, ErrorOr<Entity.Task>>
{
    public async Task<ErrorOr<Entity.Task>> Handle(GetTaskByIdQuery request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound($"Task with ID `{request.TaskId}` doesn't exist");

        return task;
    }
}