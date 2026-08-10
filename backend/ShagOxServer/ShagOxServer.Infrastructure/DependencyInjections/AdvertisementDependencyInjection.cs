using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisementsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementQueryRepository, AdvertisementQueryRepository>();
        services.AddScoped<IAdvertisementExistsRepository, AdvertisementExistsRepository>();


        services.AddScoped<IFavoriteQueryRepository, FavoriteQueryRepository>();


        services.AddScoped<IStatusQueryRepository, StatusQueryRepository>();

        return services;
    }
}