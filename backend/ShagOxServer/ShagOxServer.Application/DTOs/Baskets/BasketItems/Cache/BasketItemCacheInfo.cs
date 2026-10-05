namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
public sealed record BasketItemCacheInfo
(
    long Id,
    long BasketId,
    long AdvertVariantId
);