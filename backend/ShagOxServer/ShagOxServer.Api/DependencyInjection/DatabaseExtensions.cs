using Microsoft.EntityFrameworkCore;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

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
}