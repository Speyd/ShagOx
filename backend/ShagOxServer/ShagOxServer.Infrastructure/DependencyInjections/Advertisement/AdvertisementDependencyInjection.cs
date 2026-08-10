using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;

namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisement;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisementsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementQueryRepository, AdvertisementQueryRepository>();
        services.AddScoped<IAdvertisementExistsRepository, AdvertisementExistsRepository>();

        services.AddFavoriteInfrastructure();

        services.AddStatusInfrastructure();

        return services;
    }
}