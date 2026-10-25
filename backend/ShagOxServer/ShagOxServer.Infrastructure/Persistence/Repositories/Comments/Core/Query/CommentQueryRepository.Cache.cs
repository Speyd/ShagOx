using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Comments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;
public partial class CommentQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      ICommentQueryRepository<TEntity, TFilter>
    where TEntity : Comment
    where TFilter : BaseFilter
{
    public async Task<List<CommentCacheInfo>> GetCacheInfoByCommentableAsync(
        long commentableId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Set<TEntity>()
            .Where(x =>
                x.CommentableEntityId == commentableId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}
