using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Specification.Pictures;
public static class PicturesInvalidationDependencyInjection
{
    public static IServiceCollection AddPicturesInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<AvatarInvalidationService>();


        return services;
    }
}