using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Deliveries;
public static class DeliveriesDependencyInjections
{
    public static IServiceCollection AddDeliveriesInfrastructure(
        this IServiceCollection services)
    {
        services.AddDeliveryProviderInfrastructure();

        return services;
    }
}
