using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Query;
public partial class OrderQueryRepository
    : SearchRepository<Order, OrderSearchFilter>,
      IOrderQueryRepository
{
    public OrderQueryRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<long?> GetIdByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var basketId = await _db.Orders
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(c =>
                c.OwnerId == userId,
                cancellationToken);

        return basketId?.Id;
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

    public async Task<Order?> GetByDeliveryTrackingNumberAsync(
        string trackingNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Orders
            .WithIncludes()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(c =>
                c.DeliveryTrackingNumber == trackingNumber,
                cancellationToken);
    }

    public async Task<Order?> GetByOrderNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Orders
            .WithIncludes()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(c =>
                c.OrderNumber == orderNumber,
                cancellationToken);
    }

    public async Task<Order?> GetByPaymentTransactionIdAsync(
        string paymentTransactionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Orders
            .WithIncludes()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(c =>
                c.PaymentTransactionId == paymentTransactionId,
                cancellationToken);
    }
}
