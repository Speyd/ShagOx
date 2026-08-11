using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Location.Regions.Delete;
using ShagOxServer.Application.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Location.Regions.Validator;

namespace ShagOxServer.Application.DependencyInjections.Location;
public static class RegionDependencyInjections
{
    public static IServiceCollection AddRegionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IRegionQueryService, RegionQueryService>();
        services.AddScoped<IRegionUpdateService, RegionUpdateService>();
        services.AddScoped<IRegionDeleteService, RegionDeleteService>();

        services.AddScoped<RegionValidator>();

        return services;
    }
}