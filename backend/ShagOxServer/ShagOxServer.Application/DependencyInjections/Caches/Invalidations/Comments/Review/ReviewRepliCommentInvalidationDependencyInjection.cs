using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Comments.Review;
public static class ReviewRepliCommentInvalidationDependencyInjection
{
    public static IServiceCollection AddReviewRepliCommentInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ReviewRepliCommentInvalidationService>();


        return services;
    }
}
