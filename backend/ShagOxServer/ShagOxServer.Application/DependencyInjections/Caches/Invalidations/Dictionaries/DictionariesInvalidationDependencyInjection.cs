using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Localized;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries;
public static class DictionariesInvalidationDependencyInjection
{
    public static IServiceCollection AddDictionariesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CategoryInvalidationService>();

        services.AddAttributesInvalidationApplication();


        services.AddDictionariesLocalizedInvalidationApplication();

        services.AddDictionariesTranslationInvalidationApplication();


        return services;
    }
}