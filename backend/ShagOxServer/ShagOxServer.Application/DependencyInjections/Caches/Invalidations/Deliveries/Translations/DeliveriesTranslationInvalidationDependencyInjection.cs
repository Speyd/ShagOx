using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Translations;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries.Translations;
public static class DeliveriesTranslationInvalidationDependencyInjection
{
    public static IServiceCollection AddDeliveriesTranslationInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<DeliveryProviderTranslationInvalidationService>();

        return services;
    }
}
