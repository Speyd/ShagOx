using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Services.Common.ImageLoaders;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class CloudinaryDependencyInjection
{
    public static IServiceCollection AddCloudinary(
        this IServiceCollection services)
    {
        services.AddScoped<IImageLoaderService, CloudinaryService>();

        return services;
    }
}