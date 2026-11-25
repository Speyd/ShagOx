using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;

public partial interface ILineItemAttributeQueryRepository
    : ISearchRepository<LineItemAttribute,
        LineItemAttributeSearchFilter>
{
    Task<List<LineItemAttributeCacheContext>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<LineItemAttributeCacheContext?> GetCacheInfoByAttributeDefinitionAsync(
       long attributeDefinitionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
