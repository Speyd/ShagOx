using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Comments.Review;
public static class ReviewCommentInvalidationDependencyInjection
{
    public static IServiceCollection AddReviewCommentInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ReviewCommentInvalidationService>();


        return services;
    }
}
