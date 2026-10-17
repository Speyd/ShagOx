using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures.Review;
public static class ReviewImageDependencyInjection
{
    public static IServiceCollection AddReviewImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewImageQueryService,
            ReviewImageQueryService>();

        services.AddScoped<IReviewImageCreateService,
            ReviewImageCreateService>();

        services.AddScoped<IReviewImageUpdateService,
            ReviewImageUpdateService>();

        services.AddScoped<IReviewImageDeleteService,
            ReviewImageDeleteService>();

        services.AddScoped<
            IPictureDeleteService<ReviewImage>,
            ReviewImageDeleteService>();

        services.AddScoped<
            IPictureOrderCreateService<
                ReviewImage,
                PictureOrderCreateRequest>,
            ReviewImageCreateService>();


        services.AddScoped<ReviewImageValidator>();

        return services;
    }
}
