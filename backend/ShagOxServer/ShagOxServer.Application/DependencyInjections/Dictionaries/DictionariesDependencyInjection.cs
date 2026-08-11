using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries;

namespace ShagOxServer.Application.DependencyInjections.Dictionary;
public static class DictionariesDependencyInjection
{
    public static IServiceCollection AddDictionariesApplication(
        this IServiceCollection services)
    {
        services.AddAttributeDefinitionApplication();

        services.AddCategoryApplication();

        services.AddProductTypeApplication();

        return services;
    }
}