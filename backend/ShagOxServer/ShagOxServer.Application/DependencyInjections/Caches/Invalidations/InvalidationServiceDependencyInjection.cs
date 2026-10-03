using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisement;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Auth;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Baskets;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations;
public static class InvalidationServiceDependencyInjection
{
    public static IServiceCollection AddCacheInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddAdvertisementsInvalidationApplication();

        services.AddAuthInvalidationApplication();

        services.AddBasketsInvalidationApplication();

        services.AddDictionariesInvalidationApplication();

        return services;
    }
}