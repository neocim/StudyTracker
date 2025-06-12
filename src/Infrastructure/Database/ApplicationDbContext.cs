using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Task = Domain.Entities.Task;

namespace Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}