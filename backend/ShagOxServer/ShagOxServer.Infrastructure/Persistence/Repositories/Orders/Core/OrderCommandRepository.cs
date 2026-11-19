using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core;
public class OrderCommandRepository
    : IOrderCommandRepository
{
    private readonly AppDbContext _db;

    public OrderCommandRepository(
        AppDbContext db)
    {
        _db = db;
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
            .Where(x =>
                x.Id == orderId &&
                x.PaymentStatus != OrderPaymentStatus.Paid)
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
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus != OrderStatus.Completed &&
                x.OrderStatus != OrderStatus.Cancelled &&
                x.OrderStatus != OrderStatus.Rejected &&
                x.OrderStatus != OrderStatus.Refunded)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.OrderStatus,
                        newStatus),
                cancellationToken);

        return affectedRows > 0;
    }
}
