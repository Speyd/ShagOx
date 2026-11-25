using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Extensions;
public static class ReviewRepliCommentQueryExtensions
{
    public static IQueryable<ReviewRepliComment> WithIncludes(
       this IQueryable<ReviewRepliComment> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Owner)
                .ThenInclude(x => x.Avatar)
            .Include(x => x.Replies);
    }
}
