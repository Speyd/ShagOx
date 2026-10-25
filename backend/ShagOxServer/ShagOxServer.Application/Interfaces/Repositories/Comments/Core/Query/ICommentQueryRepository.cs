using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments;
public partial interface ICommentQueryRepository<TEntity, TFilter>
    : ISearchRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    
    Task<PagedResult<TEntity>> GetByOwnerAsync(
        long ownerId,
        PaginationParams paginationParams,
        CancellationToken cancellationToken = default);

    Task<PagedResult<TEntity>> GetByCommentableAsync(
        long commentableId,
        PaginationParams paginationParams,
        CancellationToken cancellationToken = default);
}
