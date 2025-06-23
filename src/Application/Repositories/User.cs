using User = Domain.Entities.User;

namespace Application.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetByIdAsync(Guid userId);
}