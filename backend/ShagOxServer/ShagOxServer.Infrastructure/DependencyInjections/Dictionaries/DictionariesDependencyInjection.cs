using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries;
public static class DictionariesDependencyInjection
{
    public static IServiceCollection AddDictionariesInfrastructure(
        this IServiceCollection services)
    {
        services.AddAttributeInfrastructure();

        services.AddCategoryInfrastructure();

        services.AddProductTypeInfrastructure();

        return services;
    }
}