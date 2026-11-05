using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Cache;
public sealed record AdvertisementVariantCacheInfo
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);

