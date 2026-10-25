using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Extensions;
public static class CommentQueryExtensions
{
    public static IQueryable<CommentCacheInfo> SelectCacheInfo(
        this IQueryable<Comment> query)
    {
        return query.Select(x => new CommentCacheInfo(
            x.Id,
            x.CommentableEntityId));
    }
}
