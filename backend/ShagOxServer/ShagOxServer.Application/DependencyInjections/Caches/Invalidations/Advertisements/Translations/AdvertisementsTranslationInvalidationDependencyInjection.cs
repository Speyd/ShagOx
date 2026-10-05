using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Translations;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Advertisements.Translations;
public static class AdvertisementsTranslationInvalidationDependencyInjection
{
    public static IServiceCollection AddAdvertisementsTranslationInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<StatusTranslationInvalidationService>();

        return services;
    }
}