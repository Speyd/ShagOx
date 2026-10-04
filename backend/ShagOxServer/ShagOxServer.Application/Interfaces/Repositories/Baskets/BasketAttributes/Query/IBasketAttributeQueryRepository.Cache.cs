using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;

public partial interface IBasketAttributeQueryRepository
    : IQueryRepository<BasketAttribute,
        BasketAttributeSearchFilter>
{
    Task<List<BasketAttributeCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId);

    Task<BasketAttributeCacheInfo?> GetCacheInfoByAttributeDefinitionAsync(
       long attributeDefinitionId);
}