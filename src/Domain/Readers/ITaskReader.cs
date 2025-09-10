using Entity = Domain.Entities;

namespace Domain.Readers;

public interface ITaskReader
{
    Task<Entity.Task?> GetByIdAsync(Guid taskId);
}