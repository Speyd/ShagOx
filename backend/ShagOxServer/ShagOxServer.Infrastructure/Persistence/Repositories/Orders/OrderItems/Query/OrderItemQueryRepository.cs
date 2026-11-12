using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.OrderItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Query;
public partial class OrderItemQueryRepository
    : SearchRepository<OrderItem, OrderItemSearchFilter>,
      IOrderItemQueryRepository
{
    public OrderItemQueryRepository(
        ReplicaDbContext db)
        : base(db)
    {
    }

    public async Task<PagedResult<OrderItem>> GetByOrderAsync(
        long orderId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.OrderItems
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OrderId == orderId)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public async Task<PagedResult<OrderItem>> GetPagedAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.OrderItems
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.Order.OwnerId == userId)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}
