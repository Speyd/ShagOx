using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Location.Localized;
public static class LocationLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddLocationLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CityLocalizedInvalidationService>();

        services.AddScoped<RegionLocalizedInvalidationService>();


        return services;
    }
}