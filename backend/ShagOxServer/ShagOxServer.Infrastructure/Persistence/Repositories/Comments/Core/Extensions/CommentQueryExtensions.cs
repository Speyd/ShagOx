using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Extensions;
public static class CommentQueryExtensions
{
    public static IQueryable<CommentCacheInfo> SelectCacheInfo(
        this IQueryable<Comment> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(
            x => x.SelectCacheInfo(status));
    }

    public static CommentCacheInfo SelectCacheInfo(
        this Comment x,
        EntityStatus status = EntityStatus.Active)
    {
        return new CommentCacheInfo(
            x.Id,
            x.CommentableEntityId,
            status);
    }
}
