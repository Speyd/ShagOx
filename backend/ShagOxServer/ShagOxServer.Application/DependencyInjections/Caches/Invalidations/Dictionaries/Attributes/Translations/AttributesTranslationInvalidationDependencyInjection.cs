using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes.Translations;
public static class AttributesTranslationInvalidationDependencyInjection
{
    public static IServiceCollection AddAttributesTranslationInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<AttributeDefinitionTranslationInvalidationService>();


        return services;
    }
}