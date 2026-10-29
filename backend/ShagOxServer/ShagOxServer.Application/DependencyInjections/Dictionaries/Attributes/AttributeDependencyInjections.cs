using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeValues;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeValues.Query;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDependencyInjections
{
    public static IServiceCollection AddAttributeApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeValueService,
           AttributeValueService>();

        services.AddScoped<IAttributeValueQueryService,
           AttributeValueQueryService>();

        services.AddAttributeDefinitionApplication();

        services.AddAttributeDictionaryApplication();

        services.AddAttributeDictionaryValueApplication();

        return services;
    }
}
