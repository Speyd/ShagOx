using ShagOxServer.Application.Common.Settings.Auth;
using ShagOxServer.Application.Common.Settings.Systems;
using ShagOxServer.Application.Common.Settings.Verifivations;

namespace ShagOxServer.Api.DependencyInjection;
public static class SettingsConfigurationExtensions
{
    public static IServiceCollection AddSettingsConfiguration(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<VerificationCodeSettings>(
            config.GetSection("VerificationCode"));

        services.Configure<EmailSettings>(
            config.GetSection("Email"));

        services.Configure<SmsSettings>(
            config.GetSection("Twilio"));

        services.Configure<BackgroundServiceSettings>(
           config.GetSection("BackgroundServices"));

        services.Configure<GoogleSettings>(
           config.GetSection("Google"));

        services.Configure<UserNameSettings>(
           config.GetSection("UserNameGenerate"));

        return services;
    }
}