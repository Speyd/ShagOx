using ShagOxServer.Application.Common.Options.Auth;
using ShagOxServer.Application.Common.Options.Pictures;
using ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Common.Options.Verifivations;

namespace ShagOxServer.Api.DependencyInjection;
public static class OptionsConfigurationExtensions
{
    public static IServiceCollection AddOptionsConfiguration(
        this IServiceCollection services,
        IConfiguration config)
    {
        #region Verification Options

        services.Configure<VerificationCodeOptions>(
            config.GetSection(VerificationCodeOptions.SectionName));

        #endregion

        #region Notification Options

        services.Configure<EmailOptions>(
            config.GetSection(EmailOptions.SectionName));

        services.Configure<SmsOptions>(
            config.GetSection(SmsOptions.SectionTwilioName));

        #endregion

        #region Cleanup Options

        services.Configure<UserCleanupOptions>(
            config.GetSection(UserCleanupOptions.SectionName));

        services.Configure<VerificationCodeCleanupOptions>(
            config.GetSection(VerificationCodeCleanupOptions.SectionName));

        #endregion

        #region Authentication Options

        services.Configure<GoogleOptions>(
            config.GetSection(GoogleOptions.SectionName));

        #endregion

        #region Worker Options

        services.Configure<WebhookWorkerOptions>(
            config.GetSection(WebhookWorkerOptions.SectionName));

        services.Configure<PaymentExpirationWorkerOptions>(
            config.GetSection(PaymentExpirationWorkerOptions.SectionName));
        
        #endregion

        #region Middleware

        services.Configure<RequestTrackingOptions>(
            config.GetSection(RequestTrackingOptions.SectionName));

        services.Configure<UpdateLastSeenOptions>(
            config.GetSection(UpdateLastSeenOptions.SectionName));

        #endregion

        #region Picture Options

        services.Configure<PictureLoadeOptions>(
            config.GetSection(PictureLoadeOptions.SectionName));

        #endregion

        return services;
    }
}
