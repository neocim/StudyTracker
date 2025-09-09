namespace Application.Data;

public interface ITransaction : IAsyncDisposable, IDisposable
{
    public Task CommitAsync();
    public Task RollbackAsync();
}