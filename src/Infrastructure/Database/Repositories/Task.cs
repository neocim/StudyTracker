using Application.Repositories;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Repositories;

public class TaskRepository(ApplicationDbContext applicationDbContext) : ITaskRepository
{
    public async Task AddAsync(Entity.Task task)
    {
        await applicationDbContext.AddAsync(task);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Entity.Task task)
    {
        applicationDbContext.Update(task);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Entity.Task task)
    {
        applicationDbContext.Remove(task);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<Entity.Task?> GetByIdAsync(Guid taskId)
    {
        return await applicationDbContext.FindAsync<Entity.Task>(taskId);
    }
}