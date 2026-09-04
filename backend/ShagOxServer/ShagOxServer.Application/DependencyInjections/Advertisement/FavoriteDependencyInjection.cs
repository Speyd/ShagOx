using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
using ShagOxServer.Application.Services.Advertisements.Favorites.Create;

namespace ShagOxServer.Application.DependencyInjections.Advertisement;
public static class FavoriteDependencyInjection
{
    public static IServiceCollection AddFavoriteApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IFavoriteDeleteService, FavoriteDeleteService>();
        services.AddScoped<IFavoriteQueryService, FavoriteQueryService>();
        services.AddScoped<IFavoriteUpdateService, FavoriteUpdateService>();

        services.AddScoped<FavoriteUpdateValidator>();
        services.AddScoped<FavoriteValidator>();
        services.AddScoped<IFavoriteCreateService, FavoriteCreateService>();
        
        return services;
    }
}