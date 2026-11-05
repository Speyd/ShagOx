using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Infrastructure.ExternalServices.ImageStorage;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices;
public static class CloudinaryDependencyInjection
{
    public static IServiceCollection AddCloudinaryInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPictureLoaderService, CloudinaryImageLoader>();

        return services;
    }
}
