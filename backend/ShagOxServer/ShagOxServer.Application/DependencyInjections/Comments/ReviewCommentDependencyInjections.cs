using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Services.Comments.ReviewComments.Query;

namespace ShagOxServer.Application.DependencyInjections.Comments;
public static class ReviewCommentDependencyInjections
{
    public static IServiceCollection AddReviewCommentApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewCommentQueryService,
            ReviewCommentQueryService>();

        //services.AddScoped<ReviewCommentCreateService,
        //    ReviewCommentCreateService>();

        //services.AddScoped<IReviewCommentUpdateService,
        //    ReviewCommentUpdateService>();

        //services.AddScoped<IReviewCommentDeleteService,
        //    ReviewCommentDeleteService>();

        return services;
    }
}
