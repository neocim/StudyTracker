using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Repositories;

public class TaskRepository(ApplicationDbContext _applicationDbContext) : ITaskRepository
{
    public async Task AddAsync(Entity.Task task)
    {
        await _applicationDbContext.AddAsync(task);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Entity.Task task)
    {
        _applicationDbContext.Update(task);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Entity.Task task)
    {
        _applicationDbContext.Remove(task);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<Entity.Task?> GetByIdAsync(Guid taskId)
    {
        return await _applicationDbContext.FindAsync<Entity.Task>(taskId);
    }

    public async Task<ICollection<Entity.Task>?> GetListByOwnerIdAsync(Guid ownerId)
    {
        return await _applicationDbContext.Tasks.AsNoTracking()
            .Where(task => task.OwnerId == ownerId)
            .ToListAsync();
    }
}