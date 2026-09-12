using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemQueryRepository
    : QueryRepository<BasketItem>,
      IBasketItemQueryRepository
{
    public BasketItemQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<PagedResult<BasketItem>> GetByAdvertisementAsync(
        int advertisementId,
        PaginationParams pagination)
    {
        return await _db.BasketItems
           .Where(x => x.AdvertisementId  == advertisementId)
           .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketItem>> GetByBasketAsync(
        int basketId,
        PaginationParams pagination)
    {
        return await _db.BasketItems
           .Where(x => x.BasketId == basketId)
           .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketItem>> Search(
        BasketItemSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.BasketItems
           .Filter(filter)
           .ToPagedResultAsync(pagination);
    }
}