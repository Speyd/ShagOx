using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Location.Cities.Create;
using ShagOxServer.Application.Services.Location.Cities.Delete;
using ShagOxServer.Application.Services.Location.Cities.Query;
using ShagOxServer.Application.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Location.Regions.Create;
using ShagOxServer.Application.Services.Location.Regions.Delete;
using ShagOxServer.Application.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Location.Regions.Update;

namespace ShagOxServer.Application.DependencyInjection;
public static class LocationDependencyInjection
{
    public static IServiceCollection AddLocation(this IServiceCollection services)
    {
        // Region
        services.AddScoped<IRegionQueryService, RegionQueryService>();
        services.AddScoped<IRegionCreateService, RegionCreateService>();
        services.AddScoped<IRegionUpdateService, RegionUpdateService>();
        services.AddScoped<IRegionDeleteService, RegionDeleteService>();

        // City
        services.AddScoped<ICityQueryService, CityQueryService>();
        services.AddScoped<ICityCreateService, CityCreateService>();
        services.AddScoped<ICityUpdateService, CityUpdateService>();
        services.AddScoped<ICityDeleteService, CityDeleteService>();

        return services;
    }
}