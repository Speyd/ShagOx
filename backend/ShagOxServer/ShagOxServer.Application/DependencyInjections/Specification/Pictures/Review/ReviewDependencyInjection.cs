using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures.Review;
public static class ReviewDependencyInjection
{
    public static IServiceCollection AddReviewImagesApplication(
        this IServiceCollection services)
    {
        services.AddReviewImageApplication();

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
