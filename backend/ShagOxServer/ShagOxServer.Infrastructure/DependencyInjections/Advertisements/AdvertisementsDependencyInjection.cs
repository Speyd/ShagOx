using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Infrastructure.DependencyInjections.Advertisement;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisements;
public static class AdvertisementsDependencyInjection
{
    public static IServiceCollection AddAdvertisementsInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementQueryRepository,
            AdvertisementQueryRepository>();

        services.AddScoped<IAdvertisementExistsRepository, 
            AdvertisementExistsRepository>();


        services.AddFavoriteInfrastructure();

        services.AddStatusInfrastructure();

        services.AddAdvertisementVariantInfrastructure();

        return services;
    }
}