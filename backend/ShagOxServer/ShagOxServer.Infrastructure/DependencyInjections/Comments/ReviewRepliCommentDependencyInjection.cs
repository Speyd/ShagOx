using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Comments;
public static class ReviewRepliCommentDependencyInjection
{
    public static IServiceCollection AddReviewRepliCommentInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewRepliCommentQueryRepository,
            ReviewRepliCommentQueryRepository>();

        services.AddScoped<IReviewRepliCommentExistsRepository,
            ReviewRepliCommentExistsRepository>();

        return services;
    }
}
