using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Extensions;
public static class ReviewRepliCommentQueryExtensions
{
    public static IQueryable<ReviewRepliComment> WithIncludes(
       this IQueryable<ReviewRepliComment> query)
    {
        return query
            .Include(x => x.Owner);
    }
}
