using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Attributes;
public static class AttributeDefinitionDependencyInjections
{
    public static IServiceCollection AddAttributeDefinitionInfrastructure(
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