using Application.Dto.Task.ReadModels;
using Microsoft.Extensions.DependencyInjection;
using Entity = Domain.Entities;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddAutoMapper(options =>
        {
            options.CreateMap<Entity.Task, TaskReadModel>();
        });

        return services;
    }
}