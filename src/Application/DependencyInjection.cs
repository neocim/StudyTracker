using Application.Dto.Task.ReadModels;
using AutoMapper;
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
            options.AddMaps(typeof(ApplicationMapperProfile).Assembly);
        });

        return services;
    }
}

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<Entity.Task, TaskReadModel>();
    }
}