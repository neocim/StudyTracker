using Domain.Repositories;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Repositories;

public class TaskRepository(ApplicationDbContext applicationDbContext) : ITaskRepository
{
    public ValueTask Add(Entity.Task task)
    {
        applicationDbContext.Add(task);

        return ValueTask.CompletedTask;
    }

    public ValueTask Update(Entity.Task task)
    {
        applicationDbContext.Update(task);

        return ValueTask.CompletedTask;
    }

    public ValueTask Remove(Entity.Task task)
    {
        applicationDbContext.Remove(task);

        return ValueTask.CompletedTask;
    }

    public async Task<Entity.Task?> GetByIdAsync(Guid taskId)
    {
        return await applicationDbContext.FindAsync<Entity.Task>(taskId);
    }
}