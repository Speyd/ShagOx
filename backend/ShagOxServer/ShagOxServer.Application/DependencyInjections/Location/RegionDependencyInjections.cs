using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Location.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Location;
using ShagOxServer.Application.Services.Location.Regions.Create;
using ShagOxServer.Application.Services.Location.Regions.Delete;
using ShagOxServer.Application.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.DependencyInjections.Location;
public static class RegionDependencyInjections
{
    public static IServiceCollection AddRegionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IRegionQueryService,
            RegionQueryService>();

        services.AddScoped<IRegionCreateService,
            RegionCreateService>();

        services.AddScoped<IRegionUpdateService,
            RegionUpdateService>();

        services.AddScoped<IRegionDeleteService,
            RegionDeleteService>();

        services.AddScoped<RegionValidator>();

        services.AddScoped<
            ICacheInvalidationService<Region, RegionCacheContext>,
            RegionInvalidationService>();

        services.AddRegionTranslationApplication();

        return services;
    }
}
