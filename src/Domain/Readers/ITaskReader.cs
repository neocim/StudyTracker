using Entity = Domain.Entities;

namespace Domain.Readers;

public interface ITaskReader
{
    Task<Entity.Task?> GetByIdAsync(Guid id);
    IEnumerable<Entity.Task>? GetByUserId(Guid userId);
    IEnumerable<Entity.Task>? GetSubTasksByParentId(Guid parentId);
}