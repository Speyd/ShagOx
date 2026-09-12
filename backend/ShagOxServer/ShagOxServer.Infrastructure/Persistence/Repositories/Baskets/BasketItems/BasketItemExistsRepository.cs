using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemExistsRepository
    : ExistsRepository<BasketItem>,
      IBasketItemExistsRepository
{
    public BasketItemExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        int advertisementId,
        int basketId)
    {
        return await _db.BasketItems
            .AnyAsync(c => 
                c.AdvertisementId == advertisementId &&
                c.BasketId == basketId
            );               
    }

    public async Task<bool> ExistsByAdvertisementAsync(
        int itemId,
        int advertisementId)
    {
        return await _db.BasketItems
            .AnyAsync(c =>
                c.Id == itemId &&
                c.AdvertisementId == advertisementId
            );
    }

    public async Task<bool> ExistsByBasketAsync(
        int itemId,
        int basketId)
    {
        return await _db.BasketItems
            .AnyAsync(c =>
                c.Id == itemId &&
                c.BasketId == basketId
            );
    }
}