using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries.Translations.Attributes;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Cache.Deliveries;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

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

        services.AddScoped<
            ICacheInvalidationService<AttributeDefinition, AttributeDefinitionCacheContext>,
            AttributeDefinitionInvalidationService>();

        services.AddAttributeTranslationApplication();

        return services;
    }
}
