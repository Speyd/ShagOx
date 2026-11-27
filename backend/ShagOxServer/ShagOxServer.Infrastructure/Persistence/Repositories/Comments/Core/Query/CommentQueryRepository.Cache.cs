using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Comments;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;
public partial class CommentQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      ICommentQueryRepository<TEntity, TFilter>
    where TEntity : Comment
    where TFilter : BaseFilter
{
    public async Task<List<CommentCacheContext>> GetCacheInfosByCommentableAsync(
        long commentableId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Set<TEntity>()
            .AsNoTracking()
            .WithStatusFilter(status)
            .Where(x =>
                x.CommentableEntityId == commentableId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CommentCacheContext>> GetCacheInfosByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Set<TEntity>()
            .AsNoTracking()
            .WithStatusFilter(status)
            .Where(x =>
                x.OwnerId == ownerId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }

    public async Task<CommentCacheContext?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var comment = await _db.Set<TEntity>()
            .AsNoTracking()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.Id == id);

        return comment?.SelectCacheInfo(status);
    }
}
