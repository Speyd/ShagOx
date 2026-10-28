using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDependencyInjections
{
    public static IServiceCollection AddAttributeApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeProcessor,
           AttributeProcessor>();

        services.AddAttributeDefinitionApplication();

        services.AddAttributeDictionaryApplication();

        services.AddAttributeDictionaryValueApplication();

        return services;
    }
}
