using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Create;
using ShagOxServer.Application.Services.Advertisements.Create.Validator;
using ShagOxServer.Application.Services.Advertisements.Delete;
using ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.Application.Services.Advertisements.Images;
using ShagOxServer.Application.Services.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Validator;

namespace ShagOxServer.Application.DependencyInjection;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisements(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementCreateService, AdvertisementCreateService>();
        services.AddScoped<IAdvertisementDeleteService, AdvertisementDeleteService>();
        services.AddScoped<IAdvertisementQueryService, AdvertisementQueryService>();
        services.AddScoped<IAdvertisementUpdateService, AdvertisementUpdateService>();

        services.AddScoped<AdvertisementCreateValidator>();
        services.AddScoped<AdvertisementUpdateValidator>();
        services.AddScoped<AdvertisementValidator>();



        services.AddScoped<IFavoriteDeleteService, FavoriteDeleteService>();
        services.AddScoped<IFavoriteQueryService, FavoriteQueryService>();
        services.AddScoped<IFavoriteUpdateService, FavoriteUpdateService>();

        services.AddScoped<FavoriteUpdateValidator>();
        services.AddScoped<FavoriteValidator>();


        services.AddScoped<IStatusQueryService, StatusQueryService>();


        services.AddScoped<IAdvertisementImageService, AdvertisementImageService>();
  

        return services;
    }
}