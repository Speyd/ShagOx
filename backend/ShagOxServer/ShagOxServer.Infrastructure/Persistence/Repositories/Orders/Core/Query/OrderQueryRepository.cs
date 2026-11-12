using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
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
        ReplicaDbContext db)
        : base(db)
    { }


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
