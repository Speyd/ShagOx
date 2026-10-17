using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures.Review;
public static class ReviewImageDependencyInjection
{
    public static IServiceCollection AddReviewImageDependencyInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewImageQueryRepository,
            ReviewImageQueryRepository>();

        return services;
    }
}
