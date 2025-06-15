using User = Domain.Entities.User;
using Entity = Domain.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task AddTaskAsync(User user, Entity.Task task);
    Task<User?> GetByIdAsync(Guid userId);
}