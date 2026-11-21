using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
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
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }

    public async Task<bool> TryMarkAsPaidAsync(
        long orderId,
        string? transactionId = null,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        // Оплатити можна лише замовлення, що ще чекає оплату. Якщо його вже
        // скасовано (покупцем, продавцем чи через прострочення), гроші
        // потрібно повернути, а не "оживляти" замовлення.
        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Pending &&
                x.PaymentStatus == OrderPaymentStatus.Pending)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.PaymentStatus,
                        OrderPaymentStatus.Paid)
                    .SetProperty(
                        x => x.PaidAt,
                        now)
                    .SetProperty(
                        x => x.PaymentTransactionId,
                        x => transactionId ?? x.PaymentTransactionId)
                    .SetProperty(
                        x => x.UpdatedAt,
                        now),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryConfirmAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        // Онлайн-замовлення продавець підтверджує лише після оплати.
        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Pending &&
                (x.PaymentMethod != PaymentMethod.Online ||
                 x.PaymentStatus == OrderPaymentStatus.Paid))
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, OrderStatus.Confirmed)
                    .SetProperty(x => x.ConfirmedAt, now)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryCloseAsync(
        long orderId,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        OrderStatus newStatus,
        OrderPaymentStatus expectedPaymentStatus,
        CancellationToken cancellationToken = default)
    {
        if (newStatus is not (OrderStatus.Cancelled or OrderStatus.Rejected))
        {
            throw new ArgumentException(
                "Only Cancelled or Rejected statuses are allowed.",
                nameof(newStatus));
        }

        var now = DateTime.UtcNow;
        var statuses = allowedFrom.ToList();

        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                x.PaymentStatus == expectedPaymentStatus &&
                statuses.Contains(x.OrderStatus))
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, newStatus)
                    .SetProperty(x => x.CancelledAt, now)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryShipAsync(
        long orderId,
        OrderStatus newStatus,
        string? trackingNumber,
        string? trackingUrl,
        CancellationToken cancellationToken = default)
    {
        if (newStatus is not (OrderStatus.Shipped or OrderStatus.ReadyForShipment))
        {
            throw new ArgumentException(
                "Only Shipped or ReadyForShipment statuses are allowed.",
                nameof(newStatus));
        }

        var now = DateTime.UtcNow;

        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Confirmed)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, newStatus)
                    .SetProperty(x => x.DeliveryTrackingNumber, trackingNumber)
                    .SetProperty(x => x.DeliveryTrackingUrl, trackingUrl)
                    .SetProperty(x => x.ShippedAt, now)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryCompleteAsync(
        long orderId,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        CancellationToken cancellationToken = default)
    {
        DateTime? now = DateTime.UtcNow;
        var statuses = allowedFrom.ToList();

        // Для оплати при отриманні гроші отримано разом із товаром,
        // тож одночасно позначаємо замовлення оплаченим.
        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                statuses.Contains(x.OrderStatus))
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, OrderStatus.Completed)
                    .SetProperty(x => x.DeliveredAt, x => x.DeliveredAt ?? now)
                    .SetProperty(
                        x => x.PaymentStatus,
                        x => x.PaymentMethod != PaymentMethod.Online
                            ? OrderPaymentStatus.Paid
                            : x.PaymentStatus)
                    .SetProperty(
                        x => x.PaidAt,
                        x => x.PaymentMethod != PaymentMethod.Online
                            ? now
                            : x.PaidAt)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<List<long>> GetSellerIdsAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        return await _db.OrderItems
            .Where(x =>
                x.ParentId == orderId &&
                x.EntityStatus == EntityStatus.Active)
            .Select(x => x.Advertisement.OwnerId)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> TryReopenForPaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Orders
            .Where(x =>
                x.Id == orderId &&
                x.PaymentMethod == PaymentMethod.Online &&
                x.OrderStatus == OrderStatus.Cancelled &&
                x.PaymentStatus == OrderPaymentStatus.Failed)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.OrderStatus,
                        OrderStatus.Pending)
                    .SetProperty(
                        x => x.PaymentStatus,
                        OrderPaymentStatus.Pending)
                    .SetProperty(
                        x => x.CancelledAt,
                        (DateTime?)null)
                    .SetProperty(
                        x => x.UpdatedAt,
                        DateTime.UtcNow),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<Order?> GetForPaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Orders
            .AsSplitQuery()
            .Include(x => x.Currency)
            .Include(x => x.DeliveryProvider)
            .Include(x => x.OrderItems
                .Where(i => i.EntityStatus == EntityStatus.Active))
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Status)
            .Include(x => x.OrderItems
                .Where(i => i.EntityStatus == EntityStatus.Active))
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Variants)
            .Include(x => x.OrderItems
                .Where(i => i.EntityStatus == EntityStatus.Active))
                .ThenInclude(x => x.AdvertisementVariant)
            .FirstOrDefaultAsync(x =>
                x.Id == orderId &&
                x.EntityStatus == EntityStatus.Active,
                cancellationToken);
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
