using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Localized;
public static class DictionariesLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddDictionariesLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CategoryLocalizedInvalidationService>();

        services.AddScoped<ProductTypeLocalizedInvalidationService>();

        services.AddAttributesLocalizedInvalidationApplication();


        return services;
    }
}