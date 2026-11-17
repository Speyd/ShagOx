using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
public sealed record BasketItemCacheInfo
(
    long Id,
    long BasketId,
    long AdvertId,
    long? AdvertVariantId,
    EntityStatus Status = EntityStatus.Active
);
