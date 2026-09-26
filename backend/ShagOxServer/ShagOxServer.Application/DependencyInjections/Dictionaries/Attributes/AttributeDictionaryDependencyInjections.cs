using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDictionaryDependencyInjections
{
    public static IServiceCollection AddAttributeDictionaryApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryQueryService,
            AttributeDictionaryQueryService>();

        services.AddScoped<IAttributeDictionaryCreateService,
            AttributeDictionaryCreateService>();

        //services.AddScoped<IAttributeDictionaryUpdateService,
        //AttributeDictionaryUpdateService>();

        //services.AddScoped<IAttributeDefinitionDeleteService, AttributeDefinitionDeleteService>();

        services.AddScoped<AttributeDictionaryValidator>();

        return services;
    }
}