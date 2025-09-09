namespace Application.Data;

public interface ITransaction : IAsyncDisposable
{
    public Task CommitAsync();
    public Task RollbackAsync();
}