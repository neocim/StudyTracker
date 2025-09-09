using Application.Dto.Task;
using Application.Data;
using MediatR;
using ErrorOr;

namespace Application.Cqrs.Commands.Task;

public record AddSubTaskCommand(SubTask SubTask)
    : IRequest<ErrorOr<Created>>;

public class AddSubTaskCommandHandler(IDataContext dataContext)
    : IRequestHandler<AddSubTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(AddSubTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task =
            await dataContext.TaskRepository.GetByIdAsync(request.SubTask.ParentTaskId);

        if (task is null)
            return Error.NotFound(
                description:
                $"Task with ID `{request.SubTask.ParentTaskId}` doesn't exist");

        task.AddSubTask(request.SubTask.ToTaskEntity());
        await dataContext.TaskRepository.Update(task);
        await dataContext.SaveChangesAsync();

        return Result.Created;
    }
}