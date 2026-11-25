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
        #region Auth

        services.AddHostedService<VerificationCodeCleanupService>();

        services.AddHostedService<PendingUserCleanupService>();

        services.AddHostedService<UserBanBackgroundService>();

        services.AddHostedService<UserDeviceBanBackgroundService>();

        #endregion

        #region Currencies
        #endregion

        #region Db

        services.AddHostedService<ReplicaHealthCheckeService>();

        #endregion

        #region Payments

        services.AddHostedService<PaymentExpirationWorker>();

        #endregion


        return services;
    }
}
