using Entity = Domain.Entities;

namespace Domain.Repositories;

public interface ITaskRepository
{
    ValueTask Add(Entity.Task task);
    ValueTask Update(Entity.Task task);
    ValueTask Remove(Entity.Task task);
}