using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Orders;
public sealed record OrderCacheContext
(
    long Id,
    long UserId,
    string OrderNumber,
    string? PaymentTransactionId,
    string? TrackingNumber,
    EntityStatus Status = EntityStatus.Active
);
