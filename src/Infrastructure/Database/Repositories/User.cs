using Application.Repositories;
using User = Domain.Entities.User;

namespace Infrastructure.Database.Repositories;

public class UserRepository(ApplicationDbContext _applicationDbContext) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await _applicationDbContext.AddAsync(user);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _applicationDbContext.Update(user);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _applicationDbContext.FindAsync<User>(userId);
    }
}