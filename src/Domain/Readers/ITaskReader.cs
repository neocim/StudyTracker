using Entity = Domain.Entities;

namespace Domain.Readers;

public interface ITaskReader
{
    Task<Entity.Task?> GetByIdAsync(Guid id);
    Task<IEnumerable<Entity.Task>?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Entity.Task>?> GetSubTasksByParentIdAsync(Guid parentId);
}