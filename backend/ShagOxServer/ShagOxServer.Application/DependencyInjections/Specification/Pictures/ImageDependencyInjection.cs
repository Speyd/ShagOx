using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class ImageDependencyInjection
{
    public static IServiceCollection AddImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementImageQueryService, 
            AdvertisementImageQueryService>();

        services.AddScoped<IAdvertisementImageCreateService, 
            AdvertisementImageCreateService>();

        services.AddScoped<IAdvertisementImageUpdateService, 
            AdvertisementImageUpdateService>();

        services.AddScoped<IAdvertisementImageDeleteService,
            AdvertisementImageDeleteService>();

        services.AddScoped<
            IPictureDeleteService<AdvertisementImage>,
            AdvertisementImageDeleteService>();

        services.AddScoped<
            IPictureOrderCreateService<
                AdvertisementImage,
                PictureOrderCreateRequest>,
            AdvertisementImageCreateService>();

        //services.AddScoped
        //    <IPictureOrderCreateService<AdvertisementImage, PictureOrderCreateRequest>,
        //    AdvertisementImageCreateService>();

        //services.AddScoped<IPictureOrderingService<Advertisement, AdvertisementImage>,
        //    PictureOrderingService<Advertisement, AdvertisementImage>>();

        services.AddScoped<AdvertisementImageValidator>();

        return services;
    }
}
