using Domain.Readers;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Readers;

public class TaskReader(ApplicationDbContext applicationDbContext) : ITaskReader
{
    public async Task<Entity.Task?> GetByIdAsync(Guid id)
    {
        return await applicationDbContext.FindAsync<Entity.Task>(id);
    }

    public IEnumerable<Entity.Task>? GetByUserId(Guid userId)
    {
        return applicationDbContext.Tasks?.Select(task => task)
            .Where(task => task.OwnerId == userId);
    }

    public IEnumerable<Entity.Task>? GetSubTasksByParentId(Guid parentId)
    {
        return applicationDbContext.Tasks?.Select(task => task)
            .Where(task => task.ParentId == parentId);
    }
}