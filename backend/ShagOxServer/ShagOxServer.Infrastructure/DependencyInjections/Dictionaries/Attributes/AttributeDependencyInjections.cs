using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDependencyInjections
{
    public static IServiceCollection AddAttributeInfrastructure(
        this IServiceCollection services)
    {
        services.AddAttributeDefinitionInfrastructure();

        services.AddAttributeDictionaryInfrastructure();

        services.AddAttributeDictionaryValueInfrastructure();

        return services;
    }
}