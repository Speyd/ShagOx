using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;
public static class PicturesDependencyInjection
{
    public static IServiceCollection AddPicturesInfrastructure(
       this IServiceCollection services)
    {
        services.AddAdvertisementImageInfrastructure();

        services.AddAvatarInfrastructure();

        return services;
    }
}
