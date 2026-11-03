using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Query;
public partial class BasketItemQueryRepository
    : SearchRepository<BasketItem, BasketItemSearchFilter>,
      IBasketItemQueryRepository
{
    public async Task<List<BasketItemCacheInfo>> GetCacheInfosByBasketAsync(
        long basketId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.BasketItems
            .WithStatusFilter(status)
            .Where(x => x.BasketId == basketId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<BasketItemCacheInfo>> GetCacheInfosByAdvertisementVariantAsync(
        long advertisementVariantId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.BasketItems
            .WithStatusFilter(status)
           .Where(x =>
                x.AdvertisementVariantId == advertisementVariantId)
           .SelectCacheInfo(status)
           .ToListAsync(cancellationToken);
    }
}
