using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems.Core;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;

public partial interface ILineItemQueryRepository<TEntity, TFilter>
    : ISearchRepository<TEntity, TFilter>
    where TEntity : LineItem
    where TFilter : BaseFilter
{
    Task<List<LineItemCacheContext>> GetCacheInfosByParentAsync(
        long parentId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<LineItemCacheContext>> GetCacheInfosByAdvertisementAsync(
        long advertisementId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<LineItemCacheContext>> GetCacheInfosByAdvertisementVariantAsync(
        long advertisementVariantId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
