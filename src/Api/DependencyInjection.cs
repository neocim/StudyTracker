using Api.Dto.Responses.Task;
using Application.Dto.Task.ReadModels;
using AutoMapper;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers().AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(options =>
        {
            options.AddMaps(typeof(ApiMapperProfile).Assembly);
        });

        return services;
    }
}

public class ApiMapperProfile : Profile
{
    public ApiMapperProfile()
    {
        CreateMap<TaskReadModel, TaskResponse>();
    }
}