using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Services.Comments.Core.Validator;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.DependencyInjections.Comments;
public static class ReviewRepliCommentDependencyInjections
{
    public static IServiceCollection AddReviewCommentApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewRepliCommentQueryService,
            ReviewRepliCommentQueryService>();

        //services.AddScoped<IReviewCommentCreateService,
        //    ReviewCommentCreateService>();

        //services.AddScoped<IReviewCommentUpdateService,
        //    ReviewCommentUpdateService>();

        //services.AddScoped<IReviewCommentDeleteService,
        //    ReviewCommentDeleteService>();

       // services.AddScoped<ReviewCommentValidator>();

        services.AddScoped<CommentValidator<ReviewComment>>();

        //services.AddScoped<
        //    BaseValidator<ReviewComment>,
        //    ReviewCommentValidator>();


        return services;
    }
}
