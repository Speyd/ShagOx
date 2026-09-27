using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;

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

        //services.AddScoped<IAttributeDictionaryUpdateService,
        //    AttributeDictionaryUpdateService>();

        //services.AddScoped<IAttributeDictionaryDeleteService,
        //    AttributeDictionaryDeleteService>();

        //services.AddScoped<AttributeDictionaryValidator>();

        return services;
    }
}