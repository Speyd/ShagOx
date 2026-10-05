using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Location.Translations;
public static class LocationTransaltionInvalidationDependencyInjection
{
    public static IServiceCollection AddLocationTransaltionInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CityTranslationInvalidationService>();

        services.AddScoped<RegionTranslationInvalidationService>();


        return services;
    }
}