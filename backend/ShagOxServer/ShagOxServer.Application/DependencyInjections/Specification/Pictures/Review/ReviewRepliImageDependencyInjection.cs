using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Query;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures.Review;
public static class ReviewRepliImageDependencyInjection
{
    public static IServiceCollection AddReviewRepliImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewRepliImageQueryService,
            ReviewRepliImageQueryService>();

        //services.AddScoped<IReviewImageCreateService,
        //    ReviewImageCreateService>();

        //services.AddScoped<IReviewImageUpdateService,
        //    ReviewImageUpdateService>();

        //services.AddScoped<IReviewImageDeleteService,
        //    ReviewImageDeleteService>();

        //services.AddScoped<
        //    IPictureDeleteService<ReviewImage>,
        //    ReviewImageDeleteService>();

        //services.AddScoped<
        //    IPictureUpdateService<
        //        ReviewImage,
        //        PictureOrderUpdateRequest>,
        //    ReviewImageUpdateService>();

        //services.AddScoped<
        //    IPictureOrderCreateService<
        //        ReviewImage,
        //        PictureOrderCreateRequest>,
        //    ReviewImageCreateService>();


        //services.AddScoped<ReviewImageValidator>();

        return services;
    }
}
