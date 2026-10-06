using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Query;
public partial class BasketItemQueryRepository
    : QueryRepository<BasketItem, BasketItemSearchFilter>,
      IBasketItemQueryRepository
{
    public BasketItemQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    public async Task<PagedResult<BasketItem>> GetByAdvertisementVariantAsync(
        long advertisementVariantId,
        PaginationParams pagination)
    {
        return await _db.BasketItems
            .WithIncludes()
            .Where(x =>
                x.AdvertisementVariantId == advertisementVariantId)
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