using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Api.BackgroundServices;
using ShagOxServer.Infrastructure.BackgroundServices;

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
