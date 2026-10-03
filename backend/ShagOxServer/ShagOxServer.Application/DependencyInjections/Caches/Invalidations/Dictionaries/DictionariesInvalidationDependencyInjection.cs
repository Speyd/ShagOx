using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries;
public static class DictionariesInvalidationDependencyInjection
{
    public static IServiceCollection AddDictionariesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CategoryInvalidationService>();

        services.AddAttributesInvalidationApplication();


        return services;
    }
}