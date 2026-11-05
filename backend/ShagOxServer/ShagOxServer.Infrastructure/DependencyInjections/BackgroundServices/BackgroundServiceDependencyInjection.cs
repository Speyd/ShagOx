using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.BackgroundServices.Auth;
using ShagOxServer.Infrastructure.BackgroundServices.Db;

namespace ShagOxServer.Infrastructure.DependencyInjections.BackgroundServices;
public static class BackgroundServiceDependencyInjection
{
    public static IServiceCollection AddBackgroundServiceInfrastructure(
        this IServiceCollection services)
    {
        services.AddHostedService<VerificationCodeCleanupService>();

        services.AddHostedService<PendingUserCleanupService>();

        services.AddHostedService<ReplicaHealthCheckeService>();


        return services;
    }
}
