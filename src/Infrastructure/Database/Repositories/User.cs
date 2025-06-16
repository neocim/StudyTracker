using Application.Repositories;
using User = Domain.Entities.User;

namespace Infrastructure.Database.Repositories;

public class UserRepository(ApplicationDbContext applicationDbContext) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await applicationDbContext.AddAsync(user);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        applicationDbContext.Update(user);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await applicationDbContext.FindAsync<User>(userId);
    }
}