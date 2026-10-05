using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification.Localized;
public static class SpecificationLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddSpecificationLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ConditionLocalizedInvalidationService>();


        return services;
    }
}