using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries.Localized;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries;
public static class DeliveriesInvalidationDependencyInjection
{
    public static IServiceCollection AddDeliveriesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<DeliveryProviderInvalidationService>();

        services.AddDeliveriesTranslationInvalidationApplication();

        services.AddDeliveriesLocalizedInvalidationApplication();


        return services;
    }
}
