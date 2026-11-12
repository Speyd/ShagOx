using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Orders.Core.Cache;
public sealed record OrderCacheInfo
(
    long Id,
    long UserId,
    string OrderNumber,
    string? PaymentTransactionId,
    string? TrackingNumber,
    EntityStatus Status = EntityStatus.Active
);
