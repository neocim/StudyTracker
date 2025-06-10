using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Pipiska> Pipiskas { get; set; } = null!;
}

public class Pipiska
{
    public string? Name { get; set; }
    public int Id { get; set; }
}
