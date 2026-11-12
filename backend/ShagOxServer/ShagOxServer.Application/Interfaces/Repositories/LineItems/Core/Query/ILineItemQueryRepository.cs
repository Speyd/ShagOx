using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems.Core;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;
public partial interface ILineItemQueryRepository<TEntity, TFilter>
    : ISearchRepository<TEntity, TFilter>
    where TEntity : LineItem
    where TFilter : BaseFilter
{
    Task<PagedResult<TEntity>> GetPagedAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<TEntity>> GetByParentAsync(
        long parentId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<TEntity>> GetByDependencyAsync(
        long advertisementId,
        long? advertisementVariantId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
