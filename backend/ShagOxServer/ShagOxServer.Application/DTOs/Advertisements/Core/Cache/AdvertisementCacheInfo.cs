namespace ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
public sealed record AdvertisementCacheInfo
(
    long Id,
    long SellerId,
    long? BuyerId,
    List<long> Variants
);