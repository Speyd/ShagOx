using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices;
public static class ExternalServicesDependencyInjection
{
    public static IServiceCollection AddExternalServicesInfrastructure(
        this IServiceCollection services)
    {
        services.AddCloudinaryInfrastructure();

        services.AddCurrenciesInfrastructure();

        return services;
    }
}
