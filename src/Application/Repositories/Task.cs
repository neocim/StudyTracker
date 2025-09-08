using Entity = Domain.Entities;
using ErrorOr;

namespace Application.Repositories;

public interface ITaskRepository
{
    Task<ErrorOr<Created>> AddAsync(Entity.Task task);
    Task UpdateAsync(Entity.Task task);
    Task RemoveAsync(Entity.Task task);

    Task<Entity.Task?> GetByIdAsync(Guid taskId);
}