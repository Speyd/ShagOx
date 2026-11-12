using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Query;
public partial class BasketItemQueryRepository
    : LineItemQueryRepository<BasketItem, BasketItemSearchFilter>,
      IBasketItemQueryRepository
{
    public BasketItemQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { 
    }

    public override async Task<PagedResult<BasketItem>> GetPagedAsync(
        long userId, 
        PaginationParams pagination, 
        CancellationToken cancellationToken = default, 
        EntityStatus? status = null)
    {
        return await _db.BasketItems
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.Parent.OwnerId == userId)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}
