using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Infrastructure.DependencyInjections.Advertisement.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;

namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisement;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisementInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementQueryRepository, AdvertisementQueryRepository>();
        services.AddScoped<IAdvertisementExistsRepository, AdvertisementExistsRepository>();

        services.AddFavoriteInfrastructure();

        services.AddStatusInfrastructure();

        return services;
    }
}