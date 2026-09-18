using ShagOxServer.Application.Common.Settings;

namespace ShagOxServer.Api.DependencyInjection;
public static class SettingsConfigurationExtensions
{
    public static IServiceCollection AddSettingsConfiguration(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<VerificationCodeSettings>(
            config.GetSection("VerificationCode"));

        services.Configure<BackgroundServiceSettings>(
           config.GetSection("BackgroundServices")); 

        return services;
    }
}