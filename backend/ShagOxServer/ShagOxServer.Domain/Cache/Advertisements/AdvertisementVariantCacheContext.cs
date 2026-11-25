using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Advertisements;
public sealed record AdvertisementVariantCacheContext
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);

