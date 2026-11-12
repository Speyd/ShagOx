using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries.Localized;
public static class DeliveriesLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddDeliveriesLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<DeliveryProviderLocalizedInvalidationService>();


        return services;
    }
}
