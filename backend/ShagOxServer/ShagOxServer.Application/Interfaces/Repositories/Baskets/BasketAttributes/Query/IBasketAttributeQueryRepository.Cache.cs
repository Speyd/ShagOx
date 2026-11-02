using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;

public partial interface IBasketAttributeQueryRepository
    : ISearchRepository<BasketAttribute,
        BasketAttributeSearchFilter>
{
    Task<List<BasketAttributeCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<BasketAttributeCacheInfo?> GetCacheInfoByAttributeDefinitionAsync(
       long attributeDefinitionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
