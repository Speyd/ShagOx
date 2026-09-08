using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries;
public static class AttributeDependencyInjections
{
    public static IServiceCollection AddAttributeInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDefinitionQueryRepository, 
            AttributeDefinitionQueryRepository>();

        services.AddScoped<IAttributeDefinitionExistsRepository,
            AttributeDefinitionExistsRepository>();

        services.AddAttributeTranslationInfrastructure();

        return services;
    }
}