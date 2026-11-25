using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Db;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Core.Invalidation;
using ShagOxServer.Domain.Cache;
using ShagOxServer.Domain.Entities.Db;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Api.DependencyInjection;
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<LoadBalancerOptions>(
          config.GetSection(LoadBalancerOptions.SectionName));

        services.Configure<DatabaseHealthCheckOptions>(
          config.GetSection(DatabaseHealthCheckOptions.SectionName));


        #region Primary and Cache

        services.AddScoped<ICacheInvalidationState,
            CacheInvalidationState>();

        services.AddTransient(typeof(CacheInvalidationHandler<,>));

        services.AddScoped<ICacheInvalidationDispatcher,
            CacheInvalidationDispatcher>();

        services.AddScoped<ICacheInvalidationDispatcher,
            CacheInvalidationDispatcher>();

        services.AddScoped<CacheInvalidationInterceptor>();
        services.AddScoped<CacheTransactionInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseNpgsql(
                config.GetConnectionString("PrimaryConnection"),
                npgsql => npgsql.UseQuerySplittingBehavior(
                    QuerySplittingBehavior.SplitQuery
                )
            );

            options.AddInterceptors(
                sp.GetRequiredService<CacheInvalidationInterceptor>(),
                sp.GetRequiredService<CacheTransactionInterceptor>()
            );
        });

        #endregion

        #region Replica

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

        #endregion

        return services;
    }
}
