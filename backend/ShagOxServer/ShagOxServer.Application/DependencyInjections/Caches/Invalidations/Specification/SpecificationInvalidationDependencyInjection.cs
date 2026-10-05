using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification.Localized;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification;
public static class SpecificationInvalidationDependencyInjection
{
    public static IServiceCollection AddSpecificationInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ConditionInvalidationService>();

        services.AddScoped<CurrencyInvalidationService>();

        services.AddPicturesInvalidationApplication();

        services.AddSpecificationTransaltionInvalidationApplication();

        services.AddSpecificationLocalizedInvalidationApplication();


        return services;
    }
}