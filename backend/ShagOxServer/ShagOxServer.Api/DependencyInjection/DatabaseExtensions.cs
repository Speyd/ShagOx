using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Common.Settings.Db;
using ShagOxServer.Domain.Entities.Db;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Api.DependencyInjection;
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<LoadBalancerSettings>(
          config.GetSection("LoadBalancer"));

        services.Configure<DatabaseHealthCheckSettings>(
          config.GetSection("DatabaseHealthCheck"));


        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("PrimaryConnection")));

        var connectionSection = config
            .GetSection("ConnectionStrings:ReplicaConnections");

        var replicas = connectionSection
            .GetChildren()
            .Where(x => !string.IsNullOrWhiteSpace(x.Value))
            .Select(x =>
            {
                var connectionString = x.Value!;

                var containerName = config[
                    $"Postgres:ReplicaContainers:{x.Key}"];

                if (string.IsNullOrWhiteSpace(containerName))
                {
                    throw new InvalidOperationException(
                        $"Container name is not specified for replica '{x.Key}'.");
                }

                return new Replica(
                    connectionString,
                    containerName);
            })
            .ToList();

        services.AddSingleton<IReadOnlyList<Replica>>(replicas);

        return services;
    }
}
