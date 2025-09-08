using Entity = Domain.Entities;

namespace Application.Repositories;

public interface ITaskRepository
{
    Task AddAsync(Entity.Task task);
    Task UpdateAsync(Entity.Task task);
    Task RemoveAsync(Entity.Task task);

    Task<Entity.Task?> GetByIdAsync(Guid taskId);
}