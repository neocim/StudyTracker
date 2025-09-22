using Application.Data;
using Domain.Repositories;

namespace Infrastructure.Database.Data;

public class DataContext : IDataContext
{
    public ITaskRepository TaskRepository { get; }

    private readonly ApplicationDbContext _applicationDbContext;

    public DataContext(ITaskRepository taskRepository,
        ApplicationDbContext applicationDbContext)
    {
        TaskRepository = taskRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<ITransaction> BeginTransactionAsync()
    {
        var transactionContext =
            await _applicationDbContext.Database.BeginTransactionAsync();

        return new Transaction(_applicationDbContext, transactionContext);
    }
}