using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Translations;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification.Translations;
public static class SpecificationTransaltionInvalidationDependencyInjection
{
    public static IServiceCollection AddSpecificationTransaltionInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ConditionTranslationInvalidationService>();


        return services;
    }
}