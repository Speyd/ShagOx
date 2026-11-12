using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.LineItems.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Baskets.Localized;
public static class BasketsLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddBasketsLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<BasketLocalizedInvalidationService>();

        services.AddScoped<BasketItemLocalizedInvalidationService>();


        return services;
    }
}
