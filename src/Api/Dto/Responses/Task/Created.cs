using Entity = Domain.Entities;

namespace Api.Dto.Responses.Task;

public record TaskResponse(
    Guid Id,
    Guid OwnerId,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success)
{
    public static TaskResponse FromTaskEntity(Entity.Task task)
    {
        return new TaskResponse(task.Id, task.OwnerId, task.BeginDate,
            task.EndDate, task.Name,
            task.Description, task.Success);
    }
}