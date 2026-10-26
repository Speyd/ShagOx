using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewRepliImages;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewRepliImages.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures.Review;
public static class ReviewRepliImageDependencyInjection
{
    public static IServiceCollection AddReviewRepliImageDependencyInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewRepliImageQueryRepository,
            ReviewRepliImageQueryRepository>();

        return services;
    }
}
