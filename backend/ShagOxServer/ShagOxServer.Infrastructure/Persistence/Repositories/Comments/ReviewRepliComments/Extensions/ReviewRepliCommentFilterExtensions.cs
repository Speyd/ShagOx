using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Extensions;
public static class ReviewRepliCommentFilterExtensions
{
    public static IQueryable<ReviewRepliComment> Filter(
         this IQueryable<ReviewRepliComment> query,
         CommentSearchFilter filter)
    {
        if (filter is null)
            return query;


        if (filter.CommentableEntityId.HasValue)
        {
            query = query.Where(u =>
                u.CommentableEntityId ==
                filter.CommentableEntityId);
        }

        if (filter.OwnerId.HasValue)
        {
            query = query.Where(u =>
                u.OwnerId == filter.OwnerId);
        }

        if (!string.IsNullOrWhiteSpace(filter.Text))
        {
            query = query.Where(u =>
                 EF.Functions.ILike(u.Text, $"%{filter.Text}%"));
        }

        if (filter.CreatedAt.HasValue)
        {
            query = query.Where(u =>
                u.CreatedAt == filter.CreatedAt);
        }

        if (filter.UpdatedAt.HasValue)
        {
            query = query.Where(u =>
                u.UpdatedAt == filter.UpdatedAt);
        }

        return query;
    }
}
