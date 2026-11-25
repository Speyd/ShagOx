using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Deliveries;
public sealed record DeliveryProviderCacheContext
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
