using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.BackgroundServices;
public static class BackgroundServiceDependencyInjection
{
    public static IServiceCollection AddBackgroundServiceInfrastructure(
        this IServiceCollection services)
    {
        services.AddVerificationBackgroundInfrastructure();

        services.AddUserBackgroundInfrastructure();

        return services;
    }
}