using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Infrastructure.ExternalServices.ImageStorage;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class CloudinaryDependencyInjection
{
    public static IServiceCollection AddCloudinary(
        this IServiceCollection services)
    {
        services.AddScoped<IImageLoaderService, CloudinaryImageLoader>();

        return services;
    }
}