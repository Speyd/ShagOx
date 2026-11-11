using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries;
public static class DeliveriesInvalidationDependencyInjection
{
    public static IServiceCollection AddDeliveriesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddDeliveryProviderInvalidationApplication();


        return services;
    }
}
