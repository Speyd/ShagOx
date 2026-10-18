using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures.Review;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;
public static class PicturesDependencyInjection
{
    public static IServiceCollection AddPicturesInfrastructure(
       this IServiceCollection services)
    {
        services.AddAdvertisementImageInfrastructure();

        services.AddAvatarInfrastructure();

        services.AddReviewImagesDependencyInfrastructure();


        return services;
    }
}
