using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Comments;
public static class ReviewCommentDependencyInjection
{
    public static IServiceCollection AddReviewCommentInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewCommentQueryRepository,
            ReviewCommentQueryRepository>();

        services.AddScoped<IReviewCommentExistsRepository,
            ReviewCommentExistsRepository>();

        return services;
    }
}
