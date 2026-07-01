using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Create;
using ShagOxServer.Application.Services.Advertisements.Delete;
using ShagOxServer.Application.Services.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Update;

namespace ShagOxServer.Application.DependencyInjection;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisements(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementCreateService, AdvertisementCreateService>();
        services.AddScoped<IAdvertisementDeleteService, AdvertisementDeleteService>();
        services.AddScoped<IAdvertisementQueryService, AdvertisementQueryService>();
        services.AddScoped<IAdvertisementUpdateService, AdvertisementUpdateService>();

        return services;
    }
}