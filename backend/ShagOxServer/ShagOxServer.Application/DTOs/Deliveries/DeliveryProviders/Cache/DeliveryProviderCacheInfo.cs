using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Cache;
public sealed record DeliveryProviderCacheInfo
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
