using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisement;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Auth;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Baskets;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Location;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification;

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

        services.AddLocationInvalidationApplication();

        services.AddSpecificationInvalidationApplication();


        return services;
    }
}