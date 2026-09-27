using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemQueryRepository
    : QueryRepository<BasketItem, BasketItemSearchFilter>,
      IBasketItemQueryRepository
{
    public BasketItemQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<BasketItem> ApplyIncludes(
        IQueryable<BasketItem> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<BasketItem> ApplyFilter(
      IQueryable<BasketItem> query,
      BasketItemSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<PagedResult<BasketItem>> GetByAdvertisementAsync(
        long advertisementId,
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Where(x => x.AdvertisementId  == advertisementId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketItem>> GetByBasketAsync(
        long basketId,
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Where(x => x.BasketId == basketId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketItem>> GetPagedAsync(
        long userId, 
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Where(x => x.Basket.UserId == userId)
            .ToPagedResultAsync(pagination);
    }
}