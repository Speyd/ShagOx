using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Cache;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Extensions;
public static class ReviewCommentQueryExtensions
{
    public static IQueryable<ReviewComment> WithIncludes(
       this IQueryable<ReviewComment> query)
    {
        return query
            .Include(x => x.Owner);
    }

    public static IQueryable<ReviewCommentCacheInfo> SelectCacheInfo(
        this IQueryable<ReviewComment> query)
    {
        return query.Select(x => new ReviewCommentCacheInfo(
            x.Id,
            x.CommentableEntityId));
    }
}
