using System.Security.Cryptography;
using Api.Dto.Responses.Task;
using Application.Dto.Task.ReadModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(configuration["Auth0:SigningKey"]!);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                options.Audience = configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(rsa)
                };
            });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<TaskReadModel, TaskResponse>();
    }
}