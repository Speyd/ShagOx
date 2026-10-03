using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes.Localized;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes;
public static class AttributesInvalidationDependencyInjection
{
    public static IServiceCollection AddAttributesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<AttributeDefinitionInvalidationService>();

        services.AddAttributesTranslationInvalidationApplication();

        services.AddDictionariesLocalizedInvalidationApplication();

        return services;
    }
}