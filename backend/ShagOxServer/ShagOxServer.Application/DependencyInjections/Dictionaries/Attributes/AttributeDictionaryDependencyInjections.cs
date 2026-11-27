using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Validator;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

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

        services.AddScoped<IAttributeDictionaryUpdateService,
            AttributeDictionaryUpdateService>();

        services.AddScoped<IAttributeDictionaryDeleteService,
            AttributeDictionaryDeleteService>();

        services.AddScoped<AttributeDictionaryValidator>();

        services.AddScoped<
            ICacheInvalidationService<AttributeDictionary, AttributeDictionaryCacheContext>,
            AttributeDictionaryInvalidationService>();

        services.AddScoped<AttributeDictionaryInvalidationService>();

        return services;
    }
}
