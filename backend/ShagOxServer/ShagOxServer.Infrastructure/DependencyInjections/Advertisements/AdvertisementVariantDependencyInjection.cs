using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisement;
public static class AdvertisementVariantDependencyInjection
{
    public static IServiceCollection AddAdvertisementVariantInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementVariantQueryRepository,
            AdvertisementVariantQueryRepository>();

        services.AddScoped<IAdvertisementVariantExistsRepository,
            AdvertisementVariantExistsRepository>();

        return services;
    }
}