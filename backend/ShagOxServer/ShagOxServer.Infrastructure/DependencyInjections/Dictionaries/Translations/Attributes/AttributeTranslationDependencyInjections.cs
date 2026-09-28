using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations.Attributes;
public static class AttributeTranslationDependencyInjections
{
    public static IServiceCollection AddAttributeTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddAttributeDefinitionTranslationInfrastructure();

        services.AddAttributeDictionaryValueTranslationInfrastructure();

        return services;
    }
}