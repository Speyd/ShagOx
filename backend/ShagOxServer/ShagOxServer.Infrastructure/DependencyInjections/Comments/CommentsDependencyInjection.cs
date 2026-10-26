using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Comments;
public static class CommentsDependencyInjection
{
    public static IServiceCollection AddCommentsInfrastructure(
        this IServiceCollection services)
    {
        services.AddReviewCommentInfrastructure();

        services.AddReviewRepliCommentInfrastructure();


        return services;
    }
}
