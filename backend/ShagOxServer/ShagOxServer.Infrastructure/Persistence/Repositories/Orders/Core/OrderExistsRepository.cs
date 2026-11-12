using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core;
public class OrderExistsRepository
    : ExistsOwnerRepository<Order>,
      IOrderExistsRepository
{
    public OrderExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByDeliveryTrackingNumberAsync(
        string trackingNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Orders
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.DeliveryTrackingNumber == trackingNumber,
                cancellationToken);
    }

    public async Task<bool> ExistsByOrderNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Orders
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.OrderNumber == orderNumber,
                cancellationToken);
    }

    public async Task<bool> ExistsByPaymentTransactionIdAsync(
        string paymentTransactionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Orders
            .WithStatusFilter(status)
            .AnyAsync(c => 
                c.PaymentTransactionId == paymentTransactionId,
                cancellationToken);
    }
}
