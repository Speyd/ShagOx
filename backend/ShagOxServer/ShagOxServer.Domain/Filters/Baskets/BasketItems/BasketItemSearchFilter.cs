namespace ShagOxServer.Domain.Filters.Baskets.BasketItems;
public sealed record BasketItemSearchFilter
(
    int? BasketId,
    int? AdvertisementId,
    int? Quantity
);