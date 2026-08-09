using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisementsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementQueryRepository, AdvertisementQueryRepository>();
        services.AddScoped<IAdvertisementExistsRepository, AdvertisementExistsRepository>();

        services.AddScoped<IFavoriteQueryRepository, FavoriteQueryRepository>();

        return services;
    }
}