using Domain.Readers;
using Microsoft.EntityFrameworkCore;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Readers;

public class TaskReader(ApplicationDbContext applicationDbContext) : ITaskReader
{
    public async Task<Entity.Task?> GetByIdAsync(Guid id)
    {
        return await applicationDbContext.FindAsync<Entity.Task>(id);
    }

    public async Task<IEnumerable<Entity.Task>?> GetByUserIdAsync(Guid userId)
    {
        return await applicationDbContext.Tasks
            .Select(task => task)
            .Where(task => task.OwnerId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entity.Task>?> GetSubTasksByParentIdAsync(Guid parentId)
    {
        return await applicationDbContext.Tasks
            .Select(task => task)
            .Where(task => task.ParentId == parentId)
            .ToListAsync();
    }
}