using Entity = Domain.Entities;

namespace Application.Dto.Task;

public record SubTask(
    Guid Id,
    DateOnly BeginDate,
    DateOnly EndDate,
    string? Description,
    string? Name,
    bool? Success)
{
    public Entity.Task ToTaskEntity(Guid ownerId)
    {
        return new Entity.Task(Id, ownerId, BeginDate, EndDate, Description, Name,
            Success);
    }
}