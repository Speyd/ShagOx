using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Baskets;
public static class BasketsInvalidationDependencyInjection
{
    public static IServiceCollection AddBasketsInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<BasketAttributeInvalidationService>();


        return services;
    }
}