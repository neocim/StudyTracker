using Application.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database.Data;

public class Transaction : ITransaction
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IDbContextTransaction _transactionContext;

    public Transaction(ApplicationDbContext applicationDbContext,
        IDbContextTransaction transactionContext)
    {
        _applicationDbContext = applicationDbContext;
        _transactionContext = transactionContext;
    }

    public async Task CommitAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
        await _transactionContext.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transactionContext.RollbackAsync();
    }

    public void Dispose()
    {
        _transactionContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _transactionContext.DisposeAsync();
    }
}