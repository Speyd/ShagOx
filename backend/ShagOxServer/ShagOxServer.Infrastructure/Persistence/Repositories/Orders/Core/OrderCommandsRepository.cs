using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Devices;
using ShagOxServer.Domain.Cache.Orders;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core;
public class OrderCommandsRepository
    : CommandRepository<Order>,
    IOrderCommandsRepository
{
    private readonly ICacheInvalidationService<
        Order, OrderCacheContext> _entityInvalid;


    public OrderCommandsRepository(
        ICacheInvalidationService<
            Order, OrderCacheContext> entityInvalid,
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
        _entityInvalid = entityInvalid;
    }


    public async Task<bool> TryMarkAsPaidAsync(
    long orderId,
    string? transactionId = null,
    CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Pending &&
                x.PaymentStatus == OrderPaymentStatus.Pending);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.PaymentStatus, OrderPaymentStatus.Paid)
                    .SetProperty(x => x.PaidAt, now)
                    .SetProperty(
                        x => x.PaymentTransactionId,
                        x => transactionId ?? x.PaymentTransactionId)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }


    public async Task<bool> TryMarkAsFailedAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                x.PaymentStatus == OrderPaymentStatus.Pending);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.PaymentStatus, OrderPaymentStatus.Failed)
                    .SetProperty(x => x.OrderStatus, OrderStatus.Cancelled),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }


    public async Task<bool> TryConfirmAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Pending &&
                (x.PaymentMethod != PaymentMethod.Online ||
                 x.PaymentStatus == OrderPaymentStatus.Paid));

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, OrderStatus.Confirmed)
                    .SetProperty(x => x.ConfirmedAt, now)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
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

        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                x.PaymentStatus == expectedPaymentStatus &&
                statuses.Contains(x.OrderStatus));

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, newStatus)
                    .SetProperty(x => x.CancelledAt, now)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
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

        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Confirmed);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, newStatus)
                    .SetProperty(x => x.DeliveryTrackingNumber, trackingNumber)
                    .SetProperty(x => x.DeliveryTrackingUrl, trackingUrl)
                    .SetProperty(x => x.ShippedAt, now)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }


    public async Task<bool> TryCompleteAsync(
        long orderId,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var statuses = allowedFrom.ToList();

        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                statuses.Contains(x.OrderStatus));

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
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
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }


    public async Task<bool> TryReopenForPaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                x.PaymentMethod == PaymentMethod.Online &&
                x.OrderStatus == OrderStatus.Cancelled &&
                x.PaymentStatus == OrderPaymentStatus.Failed);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, OrderStatus.Pending)
                    .SetProperty(x => x.PaymentStatus, OrderPaymentStatus.Pending)
                    .SetProperty(x => x.CancelledAt, (DateTime?)null)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }


    public async Task<bool> TryMarkAsCancelledAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Orders
            .AsNoTracking()
            .Where(x =>
                x.Id == orderId &&
                x.OrderStatus == OrderStatus.Cancelled);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.CancelledAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }


    public async Task<bool> TryUpdatePaymentStatusAsync(
        long orderId,
        OrderPaymentStatus newStatus,
        CancellationToken cancellationToken = default)
    {
        if (newStatus is not (OrderPaymentStatus.Failed or OrderPaymentStatus.Refunded))
        {
            throw new ArgumentException(
                "Only Failed or Refunded statuses are allowed.",
                nameof(newStatus));
        }

        var now = DateTime.UtcNow;

        var query = _db.Orders
            .AsNoTracking()
            .Where(x => x.Id == orderId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.PaymentStatus, newStatus)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
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

        var now = DateTime.UtcNow;

        var query = _db.Orders
            .AsNoTracking()
            .Where(x => x.Id == orderId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.OrderStatus, newStatus)
                    .SetProperty(x => x.UpdatedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }
}
