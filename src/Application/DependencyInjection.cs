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

        services.AddAutoMapper(options => { options.AddProfilesFromAssemblies(); });

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

public static class AutoMapperExtension
{
    public static void AddProfilesFromAssemblies(
        this IMapperConfigurationExpression configuration)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var profiles =
                assembly.ExportedTypes.Where(t => typeof(Profile).IsAssignableFrom(t))
                    .Select(Activator.CreateInstance).OfType<Profile>();

            configuration.AddProfiles(profiles);
        }
    }
}