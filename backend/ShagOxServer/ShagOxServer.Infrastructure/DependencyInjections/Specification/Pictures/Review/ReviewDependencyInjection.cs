using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification.Pictures.Review;
public static class ReviewDependencyInjection
{
    public static IServiceCollection AddReviewImagesDependencyInfrastructure(
        this IServiceCollection services)
    {
        services.AddReviewImageDependencyInfrastructure();

        services.AddReviewRepliImageDependencyInfrastructure();


        return services;
    }
}
