namespace Application.Repositories;

using Entity = Domain.Entities;

public interface ITaskRepository
{
    Task AddAsync(Entity.Task task);
    Task UpdateAsync(Entity.Task task);
    Task RemoveAsync(Entity.Task task);

    Task<Entity.Task?> GetByIdAsync(Guid taskId);
    Task<ICollection<Entity.Task>?> GetListByOwnerIdAsync(Guid ownerId);
}