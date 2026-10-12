using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
public partial interface IBasketItemQueryRepository
    : ISearchRepository<BasketItem, BasketItemSearchFilter>
{
    Task<List<BasketItemCacheInfo>> GetCacheInfosByBasketAsync(
        long basketId);

    Task<List<BasketItemCacheInfo>> GetCacheInfosByAdvertisementVariantAsync(
        long advertisementVariantId);
}
