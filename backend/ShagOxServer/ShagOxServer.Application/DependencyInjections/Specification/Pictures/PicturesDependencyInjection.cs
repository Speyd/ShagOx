using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Specification.Pictures.Review;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class PicturesDependencyInjection
{
    public static IServiceCollection AddPicturesApplication(
        this IServiceCollection services)
    {
        services.AddScoped<PictureValidator>();

        services.AddScoped(
            typeof(IPictureOrderingService<,>),
            typeof(PictureOrderingService<,>));

        //services.AddScoped(
        //    typeof(IPictureCreateService<,>),
        //    typeof(PictureOrderingService<,>));

        services.AddAdvertisementImageApplication();

        services.AddAvatarApplication();

        services.AddReviewImagesApplication();


        return services;
    }
}
