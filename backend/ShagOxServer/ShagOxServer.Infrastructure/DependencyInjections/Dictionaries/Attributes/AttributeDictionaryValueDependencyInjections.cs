using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDictionaryValueDependencyInjections
{
    public static IServiceCollection AddAttributeDictionaryValueInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryValueQueryRepository,
            AttributeDictionaryValueQueryRepository>();

        services.AddScoped<IAttributeDictionaryValueExistsRepository,
            AttributeDictionaryValueExistsRepository>();

        return services;
    }
}