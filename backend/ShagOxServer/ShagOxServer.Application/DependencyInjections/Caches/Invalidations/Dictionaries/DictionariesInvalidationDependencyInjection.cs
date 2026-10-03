using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries.Attributes;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Dictionaries;
public static class DictionariesInvalidationDependencyInjection
{
    public static IServiceCollection AddDictionariesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddAttributesInvalidationApplication();


        return services;
    }
}