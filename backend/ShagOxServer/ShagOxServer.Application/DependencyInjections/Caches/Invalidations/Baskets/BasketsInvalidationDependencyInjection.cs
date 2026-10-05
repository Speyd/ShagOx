using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Baskets.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Baskets;
public static class BasketsInvalidationDependencyInjection
{
    public static IServiceCollection AddBasketsInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<BasketInvalidationService>();

        services.AddScoped<BasketItemInvalidationService>();

        services.AddScoped<BasketItemBasketInvalidationService>();

        services.AddScoped<BasketAttributeInvalidationService>();

        services.AddBasketsLocalizedInvalidationApplication();


        return services;
    }
}