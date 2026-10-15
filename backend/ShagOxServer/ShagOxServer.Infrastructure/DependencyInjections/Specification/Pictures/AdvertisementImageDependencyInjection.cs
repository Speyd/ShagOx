using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;
public static class AdvertisementImageDependencyInjection
{
    public static IServiceCollection AddAdvertisementImageInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementImageQueryRepository, 
            AdvertisementImageQueryRepository>();

        return services;
    }
}
