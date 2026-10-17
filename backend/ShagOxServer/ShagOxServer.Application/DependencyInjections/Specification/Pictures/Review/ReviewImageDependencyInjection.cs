using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Query;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures.Review;
public static class ReviewImageDependencyInjection
{
    public static IServiceCollection AddReviewImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewImageQueryService,
            ReviewImageQueryService>();

        //services.AddScoped<IAdvertisementImageCreateService,
        //    AdvertisementImageCreateService>();

        //services.AddScoped<IAdvertisementImageUpdateService,
        //    AdvertisementImageUpdateService>();

        //services.AddScoped<IAdvertisementImageDeleteService,
        //    AdvertisementImageDeleteService>();

        //services.AddScoped<
        //    IPictureDeleteService<ReviewImage>,
        //    AdvertisementImageDeleteService>();

        //services.AddScoped<
        //    IPictureOrderCreateService<
        //        AdvertisementImage,
        //        PictureOrderCreateRequest>,
        //    AdvertisementImageCreateService>();


        //services.AddScoped<AdvertisementImageValidator>();

        return services;
    }
}
