using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Infrastructure.DependencyInjections.Specification.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification;
public static class ConditionDependencyInjection
{
    public static IServiceCollection AddConditionInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IConditionQueryRepository, ConditionQueryRepository>();
        services.AddScoped<IConditionExistsRepository, ConditionExistsRepository>();

        services.AddConditionTranslationInfrastructure();

        return services;
    }
}