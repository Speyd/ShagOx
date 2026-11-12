using ShagOxServer.Application.Common.Options.Auth;
using ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Common.Settings.Verifivations;

namespace ShagOxServer.Api.DependencyInjection;
public static class SettingsConfigurationExtensions
{
    public static IServiceCollection AddSettingsConfiguration(
        this IServiceCollection services,
        IConfiguration config)
    {
        #region Verification Settings

        services.Configure<VerificationCodeSettings>(
            config.GetSection("VerificationCode"));

        #endregion

        #region Notification Settings

        services.Configure<EmailSettings>(
            config.GetSection("Email"));

        services.Configure<SmsSettings>(
            config.GetSection("Twilio"));

        #endregion

        #region Cleanup Settings

        services.Configure<UserCleanupOptions>(
            config.GetSection("UserCleanup"));

        services.Configure<VerificationCodeCleanupOptions>(
            config.GetSection("VerificationCodeCleanup"));

        #endregion

        #region Authentication Settings

        services.Configure<GoogleSettings>(
            config.GetSection("Google"));

        #endregion

        #region User Settings

        services.Configure<UserNameOptions>(
            config.GetSection("UserNameGenerate"));

        #endregion

        #region Middleware

        services.Configure<RequestTrackingOptions>(
            config.GetSection("RequestTracking"));

        services.Configure<UpdateLastSeenOptions>(
            config.GetSection("UpdateLastSeen"));

        #endregion

        return services;
    }
}
