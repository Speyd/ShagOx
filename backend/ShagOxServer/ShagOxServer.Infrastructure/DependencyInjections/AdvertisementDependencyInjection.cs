using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisementsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        services.AddScoped<IAdvertisementQueryRepository, AdvertisementQueryRepository>();
        services.AddScoped<IAdvertisementExistsRepository, AdvertisementExistsRepository>();

        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<IFavoriteQueryRepository, FavoriteQueryRepository>();
        services.AddScoped<IFavoriteExistsRepository, FavoriteExistsRepository>();

        return services;
    }
}