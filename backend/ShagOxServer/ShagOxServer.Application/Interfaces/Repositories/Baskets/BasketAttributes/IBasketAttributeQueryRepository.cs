using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
public interface IBasketAttributeQueryRepository
    : IQueryRepository<BasketAttribute, 
        BasketAttributeSearchFilter>
{
    Task<PagedResult<BasketAttribute>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination);

    Task<List<BasketAttributeCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId);

    Task<BasketAttribute?> GetByAttributeDefinitionAsync(
        long attributeDefinitionId);

    Task<BasketAttributeCacheInfo?> GetCacheInfoByAttributeDefinitionAsync(
       long attributeDefinitionId);
}