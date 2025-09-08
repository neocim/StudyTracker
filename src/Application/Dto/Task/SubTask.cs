using Entity = Domain.Entities;

namespace Application.Dto.Task;

public record SubTask(
    Guid Id,
    Guid ParentTaskId,
    Guid OwnerId,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success)
{
    public Entity.Task ToTaskEntity()
    {
        return new Entity.Task(Id, OwnerId, BeginDate, EndDate, Name, Description,
            Success);
    }
}