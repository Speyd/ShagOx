using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes.Localized;
public static class DictionariesLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddDictionariesLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<AttributeDefinitionLocalizedInvalidationService>();


        return services;
    }
}