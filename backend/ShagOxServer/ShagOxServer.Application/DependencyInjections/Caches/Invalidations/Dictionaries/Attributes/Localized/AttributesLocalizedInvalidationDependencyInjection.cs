using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes.Localized;
public static class AttributesLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddAttributesLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<AttributeDefinitionLocalizedInvalidationService>();

        services.AddScoped<AttributeDictionaryValueLocalizedInvalidationService>();


        return services;
    }
}