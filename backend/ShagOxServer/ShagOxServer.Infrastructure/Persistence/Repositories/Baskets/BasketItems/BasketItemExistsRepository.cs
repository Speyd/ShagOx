using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemExistsRepository
    : ExistsRepository<BasketItem>,
      IBasketItemExistsRepository
{
    public BasketItemExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        long advertisementId,
        long basketId)
    {
        return await _db.BasketItems
            .AnyAsync(c => 
                c.AdvertisementId == advertisementId &&
                c.BasketId == basketId
            );               
    }

    public async Task<bool> ExistsByAdvertisementAsync(
        long itemId,
        long advertisementId)
    {
        return await _db.BasketItems
            .AnyAsync(c =>
                c.Id == itemId &&
                c.AdvertisementId == advertisementId
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