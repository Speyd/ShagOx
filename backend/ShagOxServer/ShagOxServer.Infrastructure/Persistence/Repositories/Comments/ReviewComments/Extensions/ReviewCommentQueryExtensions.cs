using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Extensions;
public static class ReviewCommentQueryExtensions
{
    public static IQueryable<ReviewComment> WithIncludes(
       this IQueryable<ReviewComment> query)
    {
        return query;
    }
}
