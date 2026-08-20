using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;

namespace ShagOxServer.Application.DependencyInjections.Specification.Pictures;
public static class ImageDependencyInjection
{
    public static IServiceCollection AddImageApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IImageQueryService, ImageQueryService>();
        services.AddScoped<IImageCreateService, ImageCreateService>();
        services.AddScoped<IImageUpdateService, ImageUpdateService>();
        services.AddScoped<IImageDeleteService, ImageDeleteService>();

        services.AddScoped<ImageValidator>();

        return services;
    }
}