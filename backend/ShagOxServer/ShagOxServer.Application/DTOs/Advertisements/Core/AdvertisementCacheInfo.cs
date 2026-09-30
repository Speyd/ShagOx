using ShagOxServer.Application.DTOs.Advertisements.Favorites;

namespace ShagOxServer.Application.DTOs.Advertisements.Core;
public sealed record AdvertisementCacheInfo(
    long Id,
    long SellerId,
    long? BuyerId,
    List<FavoriteCacheInfo> Favorites,
    List<long> Variants);