using Application.Repositories;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Entity = Domain.Entities;

namespace Infrastructure.Database.Repositories;

public class TaskRepository(ApplicationDbContext applicationDbContext) : ITaskRepository
{
    public async Task<ErrorOr<Created>> AddAsync(Entity.Task task)
    {
        applicationDbContext.Add(task);

        try
        {
            await applicationDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is SqlException sqlException)
                return sqlException.Number switch
                {
                    2601 | 2627 => Error.Conflict(
                        description: $"Task with ID `{task.Id}` is already exists"),
                    _ => Error.Unexpected(
                        description: $"Database error: `{sqlException.Message}`")
                };
        }

        return Result.Created;
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