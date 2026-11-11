using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries;
public static class DeliveryProviderInvalidationDependencyInjection
{
    public static IServiceCollection AddDeliveryProviderInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<DeliveryProviderInvalidationService>();


        return services;
    }
}
