using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;


namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisement;
public static class FavoriteDependencyInjection
{
    public static IServiceCollection AddFavoriteInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IFavoriteQueryRepository, FavoriteQueryRepository>();

        return services;
    }
}