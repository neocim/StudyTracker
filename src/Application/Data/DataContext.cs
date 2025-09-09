using Domain.Repositories;

namespace Application.Data;

public interface IDataContext
{
    public ITaskRepository TaskRepository { get; }

    public Task SaveChangesAsync();
    public ITransaction BeginTransactionAsync();
}