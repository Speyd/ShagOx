using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Translations;
public static class DictionariesTranslationInvalidationDependencyInjection
{
    public static IServiceCollection AddDictionariesTranslationInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CategoryTranslationInvalidationService>();

        services.AddScoped<ProductTypeTranslationInvalidationService>();

        services.AddAttributesTranslationInvalidationApplication();


        return services;
    }
}