using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;

namespace ShagOxServer.Application.DependencyInjection;
public static class DictionaryDependencyInjection
{
    public static IServiceCollection AddDictionaries(this IServiceCollection services)
    {
        // Category
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        services.AddScoped<ICategoryCreateService, CategoryCreateService>();
        services.AddScoped<ICategoryUpdateService, CategoryUpdateService>();
        services.AddScoped<ICategoryDeleteService, CategoryDeleteService>();

        services.AddScoped<CategoryValidator>();
        services.AddScoped<CategoryUpdateValidator>();


        // AttributeDefinition
        services.AddScoped<IAttributeDefinitionQueryService, AttributeDefinitionQueryService>();
        services.AddScoped<IAttributeDefinitionCreateService, AttributeDefinitionCreateService>();
        services.AddScoped<IAttributeDefinitionUpdateService, AttributeDefinitionUpdateService>();
        services.AddScoped<IAttributeDefinitionDeleteService, AttributeDefinitionDeleteService>();

        services.AddScoped<AttributeDefinitionValidator>();
        services.AddScoped<AttributeDefinitionUpdateValidator>();

        return services;
    }
}