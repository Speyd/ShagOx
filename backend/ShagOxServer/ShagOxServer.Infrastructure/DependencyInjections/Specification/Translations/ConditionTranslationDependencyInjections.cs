using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Translations;
public static class ConditionTranslationDependencyInjections
{
    public static IServiceCollection AddConditionTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IConditionTranslationQueryRepository,
            ConditionTranslationQueryRepository>();

        services.AddScoped<IConditionTranslationExistsRepository,
            ConditionTranslationExistsRepository>();

        return services;
    }
}