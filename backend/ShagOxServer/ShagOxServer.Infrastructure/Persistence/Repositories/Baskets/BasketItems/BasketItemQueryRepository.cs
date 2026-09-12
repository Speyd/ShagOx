using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemQueryRepository
    : QueryRepository<BasketItem>,
      IBasketItemQueryRepository
{
    public BasketItemQueryRepository(AppDbContext db)
        : base(db)
    { }

    public override async Task<BasketItem?> GetByIdAsync(
       int id)
    {
        return await _db.BasketItems
            .WithIncludes()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<PagedResult<BasketItem>> GetByAdvertisementAsync(
        int advertisementId,
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Where(x => x.AdvertisementId  == advertisementId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketItem>> GetByBasketAsync(
        int basketId,
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Where(x => x.BasketId == basketId)
            .ToPagedResultAsync(pagination);
    }

    public override async Task<PagedResult<BasketItem>> GetPagedAsync(
       PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketItem>> GetPagedAsync(
        int userId, 
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Where(x => x.Basket.UserId == userId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketItem>> Search(
        BasketItemSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}