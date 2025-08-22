using Application.Repositories;
using Entity = Domain.Entities;
using MediatR;
using ErrorOr;

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
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        return task;
    }
}