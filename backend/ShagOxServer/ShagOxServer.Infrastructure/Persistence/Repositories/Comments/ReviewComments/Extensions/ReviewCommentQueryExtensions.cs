using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Extensions;
public static class ReviewCommentQueryExtensions
{
    public static IQueryable<ReviewComment> WithIncludes(
       this IQueryable<ReviewComment> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Owner)
                .ThenInclude(o => o.Avatar)
            .Include(x => x.Pictures)
            .Include(x => x.Replies);
    }
}
