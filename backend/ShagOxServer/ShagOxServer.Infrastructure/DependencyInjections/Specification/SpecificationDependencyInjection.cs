using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification;
public static class SpecificationDependencyInjection
{
    public static IServiceCollection AddSpecificationInfrastructure(this IServiceCollection services)
    {
        services.AddConditionInfrastructure();

        services.AddCurrencyInfrastructure();

        services.AddImageInfrastructure();

        return services;
    }
}