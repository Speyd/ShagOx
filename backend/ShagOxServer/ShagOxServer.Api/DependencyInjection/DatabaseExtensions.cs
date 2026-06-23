using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShagOxServer.Api.Settings;
using System.Text;

namespace SchagoxServer.Api.DependencyInjection;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddJWT(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<JwtSettings>(
            config.GetSection("Jwt")
        );

        return services;
    }
}