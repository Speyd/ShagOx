using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.DependencyInjections.Deliveries.Providers;

namespace ShagOxServer.Infrastructure.DependencyInjections.Deliveries;
public static class DeliveriesDependencyInjections
{
    public static IServiceCollection AddDeliveriesInfrastructure(
        this IServiceCollection services)
    {
        services.AddDeliveryProviderInfrastructure();

        services.AddPovidersInfrastructure();

        return services;
    }
}
