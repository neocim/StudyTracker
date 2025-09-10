using Application.Data;
using Domain.Readers;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Data;
using Infrastructure.Database.Readers;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<ITaskReader, TaskReader>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITransaction, Transaction>();
        services.AddScoped<IDataContext, DataContext>();

        return services;
    }
}