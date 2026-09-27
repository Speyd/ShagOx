using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDependencyInjections
{
    public static IServiceCollection AddAttributeApplication(
        this IServiceCollection services)
    {
        services.AddAttributeDefinitionApplication();

        services.AddAttributeDictionaryApplication();

        services.AddAttributeDictionaryValueApplication();

        return services;
    }
}