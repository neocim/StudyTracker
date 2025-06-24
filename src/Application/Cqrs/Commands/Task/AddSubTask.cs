using Application.Repositories;
using MediatR;
using ErrorOr;
using Entity = Domain.Entities;

namespace Application.Cqrs.Commands.Task;

public record AddSubTaskCommand(Guid MainTaskId, Entity.Task SubTask)
    : IRequest<ErrorOr<Created>>;

public class AddSubTaskCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<AddSubTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(AddSubTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.MainTaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.MainTaskId}` doesn't exist");

        task.AddSubTask(request.SubTask);
        await taskRepository.UpdateAsync(task);

        return Result.Created;
    }
}