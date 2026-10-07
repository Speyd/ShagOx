using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Query;
public partial class BasketItemQueryRepository
    : SearchRepository<BasketItem, BasketItemSearchFilter>,
      IBasketItemQueryRepository
{
    public async Task<List<BasketItemCacheInfo>> GetCacheInfosByBasketAsync(
        long basketId)
    {
        return await _db.BasketItems
            .Where(x => x.BasketId == basketId)
            .SelectCacheInfo()
            .ToListAsync();
    }

    public async Task<List<BasketItemCacheInfo>> GetCacheInfosByAdvertisementVariantAsync(
        long advertisementVariantId)
    {
        return await _db.BasketItems
           .Where(x =>
                x.AdvertisementVariantId == advertisementVariantId)
           .SelectCacheInfo()
           .ToListAsync();
    }
}
