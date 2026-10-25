using ShagOxServer.Application.Interfaces.Repositories.Comments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;
public partial class CommentQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      ICommentQueryRepository<TEntity, TFilter>
    where TEntity : Comment
    where TFilter : BaseFilter
{
    public CommentQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<TEntity>> GetByOwnerAsync(
        long ownerId,
        PaginationParams paginationParams,
        CancellationToken cancellationToken = default)
    {
        return await _db.Set<TEntity>()
            .Where(x => x.OwnerId == ownerId)
            .ToPagedResultAsync(paginationParams,
                 x => x.Id,
                 cancellationToken);
    }

    public async Task<PagedResult<TEntity>> GetByCommentableAsync(
        long commentableId,
        PaginationParams paginationParams,
        CancellationToken cancellationToken = default)
    {
        return await _db.Set<TEntity>()
            .Where(x => x.CommentableEntityId == commentableId)
            .ToPagedResultAsync(paginationParams,
                 x => x.Id,
                 cancellationToken);
    }
}
