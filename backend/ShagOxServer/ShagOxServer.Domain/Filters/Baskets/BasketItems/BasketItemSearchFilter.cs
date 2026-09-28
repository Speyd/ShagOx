namespace ShagOxServer.Domain.Filters.Baskets.BasketItems;
public sealed record BasketItemSearchFilter
(
    long? BasketId,
    long? AdvertisementId,
    int? Quantity
) : BaseFilter();