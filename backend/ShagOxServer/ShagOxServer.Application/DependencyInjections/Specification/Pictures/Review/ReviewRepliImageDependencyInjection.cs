using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures.Review;
public static class ReviewRepliImageDependencyInjection
{
    public static IServiceCollection AddReviewRepliImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewRepliImageQueryService,
            ReviewRepliImageQueryService>();

        services.AddScoped<IReviewRepliImageCreateService,
            ReviewRepliImageCreateService>();

        //services.AddScoped<IReviewImageUpdateService,
        //    ReviewImageUpdateService>();

        services.AddScoped<IReviewRepliImageDeleteService,
            ReviewRepliImageDeleteService>();

        services.AddScoped<
            IPictureDeleteService<ReviewRepliImage>,
            ReviewRepliImageDeleteService>();

        //services.AddScoped<
        //    IPictureUpdateService<
        //        ReviewImage,
        //        PictureOrderUpdateRequest>,
        //    ReviewImageUpdateService>();

        services.AddScoped<
            IPictureOrderCreateService<
                ReviewRepliImage,
                PictureOrderCreateRequest>,
           ReviewRepliImageCreateService>();


        services.AddScoped<ReviewRepliImageValidator>();

        return services;
    }
}
