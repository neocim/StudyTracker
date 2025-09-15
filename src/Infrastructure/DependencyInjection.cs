using System.Security.Cryptography.X509Certificates;
using Application.Data;
using Domain.Readers;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Data;
using Infrastructure.Database.Readers;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
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

        AddAuthentication(services, configuration);
        AddDatabase(services, configuration);

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
        var signingKey = new X509SecurityKey(X509CertificateLoader.LoadCertificate(
            System.Text.Encoding.UTF8.GetBytes(
                configuration["Auth:SigningCertificate"]!)));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{configuration["Auth:Domain"]}";
                options.Audience = configuration["Auth:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://{configuration["Auth:Domain"]}",

                    ValidateAudience = true,
                    ValidAudience = configuration["Auth:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey
                };
            });

        services.AddScoped<ISecurityContext, SecurityContext>();

        return services;
    }
}