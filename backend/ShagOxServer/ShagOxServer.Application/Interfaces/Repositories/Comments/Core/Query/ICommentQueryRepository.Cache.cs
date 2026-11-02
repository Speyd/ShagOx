using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments;
public partial interface ICommentQueryRepository<TEntity, TFilter>
    : ISearchRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    Task<List<CommentCacheInfo>> GetCacheInfosByCommentableAsync(
        long commentableId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<CommentCacheInfo>> GetCacheInfosByOwnerAsync(
       long ownerId,
       CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<CommentCacheInfo?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
