using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Location.Localized;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Location.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Location;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Location;
public static class LocationInvalidationDependencyInjection
{
    public static IServiceCollection AddDictionariesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<RegionInvalidationService>();

        services.AddScoped<CityInvalidationService>();

        services.AddLocationLocalizedInvalidationApplication();

        services.AddLocationTransaltionInvalidationApplication();


        return services;
    }
}