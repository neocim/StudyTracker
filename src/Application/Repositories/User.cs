using User = Domain.Entities.User;
using Entity = Domain.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetByIdAsync(Guid userId);
}