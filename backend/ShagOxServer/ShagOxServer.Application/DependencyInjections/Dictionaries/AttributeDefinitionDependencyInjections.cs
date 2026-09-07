using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries;
public static class AttributeDefinitionDependencyInjections
{
    public static IServiceCollection AddAttributeDefinitionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDefinitionQueryService, AttributeDefinitionQueryService>();
        services.AddScoped<IAttributeDefinitionUpdateService, AttributeDefinitionUpdateService>();
        services.AddScoped<IAttributeDefinitionDeleteService, AttributeDefinitionDeleteService>();

        services.AddScoped<AttributeDefinitionValidator>();
        services.AddScoped<AttributeDefinitionUpdateValidator>();

        services.AddAttributeTranslationApplication();

        return services;
    }
}