using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.CategoryImages;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.CategoryImages.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures;
public static class CategoryImageDependencyInjection
{
    public static IServiceCollection AddCategoryImageInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryImageQueryRepository,
            CategoryImageQueryRepository>();

        return services;
    }
}
