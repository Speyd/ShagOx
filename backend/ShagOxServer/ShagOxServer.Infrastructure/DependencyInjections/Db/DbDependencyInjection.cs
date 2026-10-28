using Docker.DotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Interfaces.Db;
using ShagOxServer.Infrastructure.Monitoring;
using ShagOxServer.Infrastructure.Persistence.Db;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;

namespace ShagOxServer.Infrastructure.DependencyInjections.Db;
public static class DbDependencyInjection
{
    public static IServiceCollection AddDbInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<IPostgresLoadBalancer,
            PostgresLoadBalancer>();

        services.AddDbContext<ReplicaDbContext>((sp, options) =>
        {
            var loadBalancer =
                sp.GetRequiredService<IPostgresLoadBalancer>();

            var replica = loadBalancer.SelectReplica();

            if (replica is null)
            {
                throw new InvalidOperationException(
                    "No healthy PostgreSQL replicas available.");
            }

            options.UseNpgsql(replica.ConnectionString);
            //options.UseNpgsql(replica.ConnectionString, o =>
                //o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });

        return services;
    }
}
