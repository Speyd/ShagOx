using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations.Attributes;
public static class AttributeDictionaryValueTranslationDependencyInjections
{
    public static IServiceCollection AddAttributeDictionaryValueTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryValueTranslationQueryRepository,
            AttributeDictionaryValueTranslationQueryRepository>();

        services.AddScoped<IAttributeDictionaryValueTranslationExistsRepository,
            AttributeDictionaryValueTranslationExistsRepository>();

        return services;
    }
}