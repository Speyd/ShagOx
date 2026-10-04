using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification;
public static class SpecificationInvalidationDependencyInjection
{
    public static IServiceCollection AddSpecificationInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ConditionInvalidationService>();


        return services;
    }
}