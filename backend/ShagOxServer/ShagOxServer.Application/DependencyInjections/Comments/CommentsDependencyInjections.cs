using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Comments;
public static class CommentsDependencyInjections
{
    public static IServiceCollection AddCommentsApplication(
        this IServiceCollection services)
    {
        services.AddReviewCommentApplication();

        services.AddReviewRepliCommentApplication();


        return services;
    }
}
