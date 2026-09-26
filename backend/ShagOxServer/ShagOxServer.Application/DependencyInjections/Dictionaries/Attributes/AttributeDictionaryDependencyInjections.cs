using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Query;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDictionaryDependencyInjections
{
    public static IServiceCollection AddAttributeDictionaryApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryQueryService,
            AttributeDictionaryQueryService>();
        //services.AddScoped<IAttributeDefinitionUpdateService, AttributeDefinitionUpdateService>();
        //services.AddScoped<IAttributeDefinitionDeleteService, AttributeDefinitionDeleteService>();

        //services.AddScoped<AttributeDefinitionValidator>();
        //services.AddScoped<AttributeDefinitionUpdateValidator>();

        return services;
    }
}