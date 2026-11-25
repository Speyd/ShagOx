using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Extensions;
public static class CommentQueryExtensions
{
    public static IQueryable<CommentCacheContext> SelectCacheInfo(
        this IQueryable<Comment> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(
            x => x.SelectCacheInfo(status));
    }

    public static CommentCacheContext SelectCacheInfo(
        this Comment x,
        EntityStatus status = EntityStatus.Active)
    {
        return new CommentCacheContext(
            x.Id,
            x.CommentableEntityId,
            status);
    }
}
