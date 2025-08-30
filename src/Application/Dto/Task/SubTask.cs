using Entity = Domain.Entities;

namespace Application.Dto.Task;

public record SubTask(
    Guid Id,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success)
{
    public Entity.Task ToTaskEntity(Guid ownerId)
    {
        return new Entity.Task(Id, ownerId, BeginDate, EndDate, Name, Description,
            Success);
    }
}