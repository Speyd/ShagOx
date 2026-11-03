using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Cache;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
public sealed record AdvertisementCacheInfo
(
    long Id,
    long SellerId,
    long? BuyerId,
    List<AdvertisementVariantCacheInfo> Variants,
    EntityStatus Status = EntityStatus.Active
);
