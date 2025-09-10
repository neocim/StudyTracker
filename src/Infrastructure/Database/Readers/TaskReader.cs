using Domain.Readers;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Readers;

public class TaskReader(ApplicationDbContext applicationDbContext) : ITaskReader
{
    public async Task<Entity.Task?> GetByIdAsync(Guid taskId)
    {
        return await applicationDbContext.FindAsync<Entity.Task>(taskId);
    }
}