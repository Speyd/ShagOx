using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems;
public class OrderItemExistsRepository
    : ExistsRepository<OrderItem>,
      IOrderItemExistsRepository
{
    public OrderItemExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        long orderId,
        long itemId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.OrderItems
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.Id == itemId &&
                c.OrderId == orderId
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
