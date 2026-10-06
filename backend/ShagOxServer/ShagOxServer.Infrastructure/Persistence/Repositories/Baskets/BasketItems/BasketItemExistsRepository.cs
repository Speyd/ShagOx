using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemExistsRepository
    : ExistsRepository<BasketItem>,
      IBasketItemExistsRepository
{
    public BasketItemExistsRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        long advertisementVariantId,
        long basketId)
    {
        return await _db.BasketItems
            .AnyAsync(c => 
                c.AdvertisementVariantId == advertisementVariantId &&
                c.BasketId == basketId
            );               
    }

    public async Task<bool> ExistsByAdvertisementVariantAsync(
        long itemId,
        long advertisementVariantId)
    {
        return await _db.BasketItems
            .AnyAsync(c =>
                c.Id == itemId &&
                c.AdvertisementVariantId == advertisementVariantId
            );
    }

    public async Task<bool> ExistsByBasketAsync(
        long itemId,
        long basketId)
    {
        return await _db.BasketItems
            .AnyAsync(c =>
                c.Id == itemId &&
                c.BasketId == basketId
            );
    }

    public async Task<bool> IsOwnerAsync(
        long enityId,
        long userId)
    {
        var result = await _db.BasketItems
            .WithIncludes()
            .AnyAsync(x =>
                (x.Id == enityId && x.Basket.UserId == userId));

        return result;
    }
}