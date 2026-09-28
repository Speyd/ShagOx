using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations.Attributes;
public static class AttributeDefinitionTranslationDependencyInjections
{
    public static IServiceCollection AddAttributeDefinitionTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDefinitionTranslationQueryRepository,
            AttributeDefinitionTranslationQueryRepository>();

        services.AddScoped<IAttributeDefinitionTranslationExistsRepository,
            AttributeDefinitionTranslationExistsRepository>();

        return services;
    }
}