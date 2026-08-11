using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification;
public static class ImageDependencyInjection
{
    public static IServiceCollection AddImageInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IImageQueryRepository, ImageQueryRepository>();

        return services;
    }
}