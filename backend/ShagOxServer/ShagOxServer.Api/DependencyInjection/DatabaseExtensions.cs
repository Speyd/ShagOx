using Microsoft.EntityFrameworkCore;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;

namespace ShagOxServer.Api.DependencyInjection;
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("PrimaryConnection")));

        services.AddDbContext<ReplicaDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("ReplicaConnection")));

        return services;
    }
}