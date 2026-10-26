using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Create;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Comments.Core.Validator;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Create;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Delete;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.DependencyInjections.Comments;
public static class ReviewRepliCommentDependencyInjections
{
    public static IServiceCollection AddReviewRepliCommentApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IReviewRepliCommentQueryService,
            ReviewRepliCommentQueryService>();

        services.AddScoped<IReviewRepliCommentCreateService,
            ReviewRepliCommentCreateService>();

        //services.AddScoped<IReviewCommentUpdateService,
        //    ReviewCommentUpdateService>();

        services.AddScoped<IReviewRepliCommentDeleteService,
            ReviewRepliCommentDeleteService>();

        services.AddScoped<ReviewRepliCommentValidator>();

        services.AddScoped<CommentValidator<ReviewComment>>();

        services.AddScoped<
            BaseValidator<ReviewRepliComment>,
            ReviewRepliCommentValidator>();


        return services;
    }
}
