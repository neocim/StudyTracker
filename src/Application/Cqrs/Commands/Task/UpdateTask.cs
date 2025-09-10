using Application.Data;
using Domain.Readers;
using ErrorOr;
using MediatR;

namespace Application.Cqrs.Commands.Task;

public record UpdateTaskCommand(
    Guid TaskId,
    bool? Success,
    string? Name,
    string? Description)
    : IRequest<ErrorOr<Updated>>;

public class UpdateTaskCommandHandler(IDataContext dataContext, ITaskReader taskReader)
    : IRequestHandler<UpdateTaskCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskReader.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        task.Success = request.Success ?? task.Success;
        task.Name = request.Name ?? task.Name;
        task.Description = request.Description ?? task.Description;

        await dataContext.TaskRepository.Update(task);
        await dataContext.SaveChangesAsync();

        return Result.Updated;
    }
}