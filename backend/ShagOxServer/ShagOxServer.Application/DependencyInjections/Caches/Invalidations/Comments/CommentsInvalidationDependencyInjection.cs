using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Comments.Review;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Comments;
public static class CommentsInvalidationDependencyInjection
{
    public static IServiceCollection AddCommentsInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddReviewCommentInvalidationApplication();

        services.AddReviewRepliCommentInvalidationApplication();


        return services;
    }
}
