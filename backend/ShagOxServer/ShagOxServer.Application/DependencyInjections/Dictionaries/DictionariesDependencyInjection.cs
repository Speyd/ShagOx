using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries;
using ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;

namespace ShagOxServer.Application.DependencyInjections.Dictionary;
public static class DictionariesDependencyInjection
{
    public static IServiceCollection AddDictionariesApplication(
        this IServiceCollection services)
    {
        services.AddAttributeApplication();

        services.AddCategoryApplication();

        services.AddProductTypeApplication();

        return services;
    }
}