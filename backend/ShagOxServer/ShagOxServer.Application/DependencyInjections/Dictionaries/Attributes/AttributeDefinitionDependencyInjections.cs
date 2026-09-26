using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDefinitionDependencyInjections
{
    public static IServiceCollection AddAttributeDefinitionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDefinitionQueryService,
            AttributeDefinitionQueryService>();

        services.AddScoped<IAttributeDefinitionCreateService,
           AttributeDefinitionCreateService>();

        services.AddScoped<IAttributeDefinitionUpdateService,
            AttributeDefinitionUpdateService>();

        services.AddScoped<IAttributeDefinitionDeleteService,
            AttributeDefinitionDeleteService>();

        services.AddScoped<AttributeDefinitionValidator>();
        services.AddScoped<AttributeDefinitionUpdateValidator>();

        services.AddAttributeTranslationApplication();

        return services;
    }
}