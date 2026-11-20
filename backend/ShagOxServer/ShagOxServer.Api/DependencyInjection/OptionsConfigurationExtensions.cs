using ShagOxServer.Application.Common.Options.Auth;
using ShagOxServer.Application.Common.Options.Systems;
using ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Common.Options.Verifivations;
using ShagOxServer.Infrastructure.Options;

namespace ShagOxServer.Api.DependencyInjection;
public static class OptionsConfigurationExtensions
{
    public static IServiceCollection AddOptionsConfiguration(
        this IServiceCollection services,
        IConfiguration config)
    {
        #region Verification Options

        services.Configure<VerificationCodeOptions>(
            config.GetSection("VerificationCode"));

        #endregion

        #region Notification Options

        services.Configure<EmailOptions>(
            config.GetSection("Email"));

        services.Configure<SmsOptions>(
            config.GetSection("Twilio"));

        #endregion

        #region Cleanup Options

        services.Configure<UserCleanupOptions>(
            config.GetSection("UserCleanup"));

        services.Configure<VerificationCodeCleanupOptions>(
            config.GetSection("VerificationCodeCleanup"));

        #endregion

        #region Authentication Options

        services.Configure<GoogleOptions>(
            config.GetSection("Google"));

        #endregion

        #region Worker Options

        services.Configure<WebhookWorkerOptions>(
            config.GetSection("WebhookWorker"));

        services.Configure<PaymentExpirationWorkerOptions>(
            config.GetSection("PaymentExpirationWorker"));
        
        #endregion

        #region Middleware

        services.Configure<RequestTrackingOptions>(
            config.GetSection("RequestTracking"));

        services.Configure<UpdateLastSeenOptions>(
            config.GetSection("UpdateLastSeen"));

        #endregion

        #region Picture Options

        services.Configure<PictureLoadeOptions>(
            config.GetSection("PictureLoade"));

        #endregion

        return services;
    }
}
