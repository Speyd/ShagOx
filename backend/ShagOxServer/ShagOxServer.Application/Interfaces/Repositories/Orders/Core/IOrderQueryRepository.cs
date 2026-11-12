using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.Core;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
public interface IOrderQueryRepository
    : ISearchRepository<Order, OrderSearchFilter>
{
    Task<Order?> GetByOrderNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<Order?> GetByPaymentTransactionIdAsync(
        string paymentTransactionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<Order?> GetByDeliveryTrackingNumberAsync(
        string trackingNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
