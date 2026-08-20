using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification;
public static class SpecificationDependencyInjection
{
    public static IServiceCollection AddSpecificationInfrastructure(
        this IServiceCollection services)
    {
        services.AddConditionInfrastructure();

        services.AddCurrencyInfrastructure();

        services.AddPicturesInfrastructure();

        return services;
    }
}