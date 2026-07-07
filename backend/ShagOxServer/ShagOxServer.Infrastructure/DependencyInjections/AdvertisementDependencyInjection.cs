using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisementsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        services.AddScoped<IAdvertisementQueryRepository, AdvertisementQueryRepository>();
        services.AddScoped<IAdvertisementExistsRepository, AdvertisementExistsRepository>();

        return services;
    }
}