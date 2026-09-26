using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations;
public static class AttributeTranslationDependencyInjections
{
    public static IServiceCollection AddAttributeTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDefinitionTranslationQueryRepository,
            AttributeDefinitionTranslationQueryRepository>();

        services.AddScoped<IAttributeDefinitionTranslationExistsRepository,
            AttributeDefinitionTranslationExistsRepository>();

        return services;
    }
}