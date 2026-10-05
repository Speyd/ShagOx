using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisements.Localized;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisements.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisement;
public static class AdvertisementsInvalidationDependencyInjection
{
    public static IServiceCollection AddAdvertisementsInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<AdvertisementInvalidationService>();

        services.AddScoped<AdvertisementVariantInvalidationService>();

        services.AddScoped<FavoriteInvalidationService>();

        services.AddScoped<StatusInvalidationService>();

        services.AddAdvertisementsTranslationInvalidationApplication();

        services.AddAdvertisementsLocalizedInvalidationApplication();


        return services;
    }
}