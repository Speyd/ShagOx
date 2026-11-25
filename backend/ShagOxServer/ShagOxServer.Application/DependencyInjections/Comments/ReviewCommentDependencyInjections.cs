using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Create;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Update;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Comments.Core.Validator;
using ShagOxServer.Application.Services.Comments.ReviewComments.Create;
using ShagOxServer.Application.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Services.Comments.ReviewComments.Update;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Domain.Cache.Baskets;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Comments.Review;

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

        services.AddScoped<IReviewCommentUpdateService,
            ReviewCommentUpdateService>();

        services.AddScoped<IReviewCommentDeleteService,
            ReviewCommentDeleteService>();

        services.AddScoped<ReviewCommentValidator>();

        services.AddScoped<CommentValidator<Advertisement>>();

        services.AddScoped<
            BaseValidator<ReviewComment>,
            ReviewCommentValidator>();

        services.AddScoped<
            ICacheInvalidationService<ReviewComment, CommentCacheContext>,
            ReviewCommentInvalidationService>();


        return services;
    }
}
