using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDictionaryDependencyInjections
{
    public static IServiceCollection AddAttributeDictionaryInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryQueryRepository,
            AttributeDictionaryQueryRepository>();

        services.AddScoped<IAttributeDictionaryExistsRepository,
            AttributeDictionaryExistsRepository>();

        return services;
    }
}