using Application.Data;
using Domain.Repositories;

namespace Infrastructure.Database.Data;

public class DataContext(
    ApplicationDbContext applicationDbContext,
    ITaskRepository taskRepository) : IDataContext
{
    public ITaskRepository TaskRepository => taskRepository;

    public async Task SaveChangesAsync()
    {
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<ITransaction> BeginTransactionAsync()
    {
        var transactionContext =
            await applicationDbContext.Database.BeginTransactionAsync();

        return new Transaction(applicationDbContext, transactionContext);
    }
}