using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Specification;
public static class SpecificationDependencyInjection
{
    public static IServiceCollection AddSpecificationApplication(
        this IServiceCollection services)
    {
        services.AddConditionApplication();

        services.AddCurrencyApplication();

        services.AddImageApplication();

        return services;
    }
}