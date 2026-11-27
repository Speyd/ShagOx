using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries.Translations.Attributes;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Validator;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeValues.Query;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDictionaryValueDependencyInjections
{
    public static IServiceCollection AddAttributeDictionaryValueApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryValueQueryService,
            AttributeDictionaryValueQueryService>();

        services.AddScoped<IAttributeDictionaryValueCreateService,
            AttributeDictionaryValueCreateService>();

        services.AddScoped<IAttributeDictionaryValueUpdateService,
            AttributeDictionaryValueUpdateService>();

        services.AddScoped<IAttributeDictionaryValueDeleteService,
            AttributeDictionaryValueDeleteService>();

        services.AddScoped<AttributeDictionaryValueValidator>();

        services.AddScoped<AttributeValueQueryService>();

        services.AddScoped<
            ICacheInvalidationService<AttributeDictionaryValue,
            AttributeDictionaryValueCacheContext>,
            AttributeDictionaryValueInvalidationService>();

        services.AddScoped<AttributeDictionaryValueInvalidationService>();

        services.AddAttributeValueTranslationApplication();

        return services;
    }
}
