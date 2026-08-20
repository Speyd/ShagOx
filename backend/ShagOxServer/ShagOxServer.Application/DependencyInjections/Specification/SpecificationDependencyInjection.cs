using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Specification.Pictures;

namespace ShagOxServer.Application.DependencyInjections.Specification;
public static class SpecificationDependencyInjection
{
    public static IServiceCollection AddSpecificationApplication(
        this IServiceCollection services)
    {
        services.AddConditionApplication();

        services.AddCurrencyApplication();

        services.AddPicturesApplication();

        return services;
    }
}