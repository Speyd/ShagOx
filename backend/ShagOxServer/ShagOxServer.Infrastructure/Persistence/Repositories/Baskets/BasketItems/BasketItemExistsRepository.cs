using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemExistsRepository
    : ExistsRepository<BasketItem>,
      IBasketItemExistsRepository
{
    public BasketItemExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        long advertisementVariantId,
        long basketId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.BasketItems
            .WithStatusFilter(status)
            .AnyAsync(c => 
                c.AdvertisementVariantId == advertisementVariantId &&
                c.BasketId == basketId
            );               
    }

    public async Task<bool> ExistsByAdvertisementVariantAsync(
        long itemId,
        long advertisementVariantId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.BasketItems
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.Id == itemId &&
                c.AdvertisementVariantId == advertisementVariantId
            );
    }

    public async Task<bool> ExistsByBasketAsync(
        long itemId,
        long basketId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.BasketItems
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.Id == itemId &&
                c.BasketId == basketId
            );
    }

    public async Task<bool> IsOwnerAsync(
        long enityId,
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var result = await _db.BasketItems
            .WithStatusFilter(status)
            .WithIncludes()
            .AnyAsync(x =>
                (x.Id == enityId && x.Basket.OwnerId == userId));

        return result;
    }
  
}
