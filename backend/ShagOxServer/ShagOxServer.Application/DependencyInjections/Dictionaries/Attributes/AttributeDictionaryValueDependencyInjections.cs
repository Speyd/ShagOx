using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Validator;

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

        return services;
    }
}