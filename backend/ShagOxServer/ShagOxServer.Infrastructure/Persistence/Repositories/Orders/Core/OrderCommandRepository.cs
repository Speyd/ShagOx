using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core;
public class OrderCommandRepository
    : CommandRepository<Order>,
    IOrderCommandRepository
{
    public OrderCommandRepository(
        AppDbContext db
    )
        : base(db)
    {
    }

    public async Task<bool> TryMarkAsPaidAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                x.PaymentStatus == OrderPaymentStatus.Pending)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.PaymentStatus,
                        OrderPaymentStatus.Paid),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryMarkAsCancelledAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Cancelled)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.CancelledAt,
                        DateTime.UtcNow),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryUpdatePaymentStatusAsync(
        long orderId,
        OrderPaymentStatus newStatus,
        CancellationToken cancellationToken = default)
    {
        if (newStatus is not (OrderPaymentStatus.Failed or OrderPaymentStatus.Refunded))
            throw new ArgumentException(
                "Only Failed or Refunded statuses are allowed.",
                nameof(newStatus));

        var affectedRows = await _db.Orders
            .Where(x => x.Id == orderId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.PaymentStatus,
                        newStatus),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryUpdateStatusAsync(
        long orderId,
        OrderStatus newStatus,
        CancellationToken cancellationToken = default)
    {
        if (newStatus is not (
            OrderStatus.Cancelled or
            OrderStatus.Rejected or
            OrderStatus.Refunded))
        {
            throw new ArgumentException(
                "Only Cancelled, Rejected or Refunded statuses are allowed.",
                nameof(newStatus));
        }

        var affectedRows = await _db.Orders
            .Where(x => x.Id == orderId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.OrderStatus,
                        newStatus),
                cancellationToken);

        return affectedRows > 0;
    }
}
