using Application.Data;
using Domain.Readers;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Data;
using Infrastructure.Database.Readers;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Application.Security;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        AddDatabase(services, configuration);
        AddAuthentication(services, configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<ITaskReader, TaskReader>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IDataContext, DataContext>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services,
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

        services.AddScoped<ISecurityContext, SecurityContext>();

        return services;
    }
}