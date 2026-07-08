using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Create;
using ShagOxServer.Application.Services.Advertisements.Delete;
using ShagOxServer.Application.Services.Advertisements.Favorites.Create;
using ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update;
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

        services.AddScoped<IFavoriteCreateService, FavoriteCreateService>();
        services.AddScoped<IFavoriteDeleteService, FavoriteDeleteService>();
        services.AddScoped<IFavoriteQueryService, FavoriteQueryService>();
        services.AddScoped<IFavoriteUpdateService, FavoriteUpdateService>();

        return services;
    }
}