using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisements.Localized;
public static class AdvertisementsLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddAdvertisementsLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<AdvertisementLocalizedInvalidationService>();

        services.AddScoped<StatusLocalizedInvalidationService>();


        return services;
    }
}