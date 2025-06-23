using Application.Dto.Task;
using Application.Repositories;
using MediatR;
using ErrorOr;

namespace Application.Cqrs.Queries.Task;

public record GetTaskByIdQuery(Guid TaskId) : IRequest<ErrorOr<TaskResult>>;

public class GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetTaskByIdQuery, ErrorOr<TaskResult>>
{
    public async Task<ErrorOr<TaskResult>> Handle(GetTaskByIdQuery request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        return TaskResult.FromTask(task);
    }
}