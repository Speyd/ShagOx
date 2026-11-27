using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.DependencyInjections.ExternalServices.Delivery;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices;
public static class ExternalServicesDependencyInjection
{
    public static IServiceCollection AddExternalServicesInfrastructure(
        this IServiceCollection services)
    {
        services.AddCloudinaryInfrastructure();

        services.AddCurrenciesInfrastructure();

        services.AddDeliveryInfrastructure();

        services.AddDeviceInfosInfrastructure();

        services.AddSessionInfosInfrastructure();

        return services;
    }
}
