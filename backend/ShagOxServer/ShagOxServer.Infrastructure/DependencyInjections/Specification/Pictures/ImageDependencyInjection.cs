using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;
public static class ImageDependencyInjection
{
    public static IServiceCollection AddImageInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IImageQueryRepository, ImageQueryRepository>();

        return services;
    }
}