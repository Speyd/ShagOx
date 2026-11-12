using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisement;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Auth;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Baskets;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Comments;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Deliveries;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.LineItems;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Location;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations;
public static class InvalidationServiceDependencyInjection
{
    public static IServiceCollection AddCacheInvalidationApplication(
        this IServiceCollection services)
    {
        services
            .AddAdvertisementsInvalidationApplication()
            .AddAuthInvalidationApplication()
            .AddLineItemsInvalidationApplication()
            .AddBasketsInvalidationApplication()
            .AddDictionariesInvalidationApplication()
            .AddLocationInvalidationApplication()
            .AddSpecificationInvalidationApplication()
            .AddCommentsInvalidationApplication()
            .AddDeliveriesInvalidationApplication();

        return services;
    }
}
