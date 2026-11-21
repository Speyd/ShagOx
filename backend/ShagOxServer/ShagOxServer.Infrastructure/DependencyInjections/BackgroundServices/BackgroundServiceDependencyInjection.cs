using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.BackgroundServices.Auth;
using ShagOxServer.Infrastructure.BackgroundServices.Db;
using ShagOxServer.Infrastructure.BackgroundServices.Payments;

namespace ShagOxServer.Infrastructure.DependencyInjections.BackgroundServices;
public static class BackgroundServiceDependencyInjection
{
    public static IServiceCollection AddBackgroundServiceInfrastructure(
        this IServiceCollection services)
    {
        services.AddHostedService<ReplicaHealthCheckeService>();

        services.AddHostedService<VerificationCodeCleanupService>();

        services.AddHostedService<PendingUserCleanupService>();
   

        services.AddHostedService<PaymentWebhookWorker>();

        services.AddHostedService<PaymentExpirationWorker>();


        return services;
    }
}
