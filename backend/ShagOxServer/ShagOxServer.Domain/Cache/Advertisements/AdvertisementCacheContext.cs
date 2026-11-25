using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Advertisements;
public sealed record AdvertisementCacheContext
(
    long Id,
    long SellerId,
    long? BuyerId,
    List<AdvertisementVariantCacheContext> Variants,
    EntityStatus Status = EntityStatus.Active
);
