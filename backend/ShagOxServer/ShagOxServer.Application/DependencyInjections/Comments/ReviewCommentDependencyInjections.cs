using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Create;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Services.Comments.Core.Validator;
using ShagOxServer.Application.Services.Comments.ReviewComments.Create;
using ShagOxServer.Application.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.DependencyInjections.Comments;
public static class ReviewCommentDependencyInjections
{
    public static IServiceCollection AddReviewCommentApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewCommentQueryService,
            ReviewCommentQueryService>();

        services.AddScoped<IReviewCommentCreateService,
            ReviewCommentCreateService>();

        //services.AddScoped<IReviewCommentUpdateService,
        //    ReviewCommentUpdateService>();

        services.AddScoped<IReviewCommentDeleteService,
            ReviewCommentDeleteService>();

        services.AddScoped<ReviewCommentValidator>();

        services.AddScoped<CommentValidator<Advertisement>>();


        return services;
    }
}
