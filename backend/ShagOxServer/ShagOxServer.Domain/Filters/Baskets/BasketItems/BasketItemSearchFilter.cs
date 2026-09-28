namespace ShagOxServer.Domain.Filters.Baskets.BasketItems;
public sealed record BasketItemSearchFilter
(
    long? BasketId,
    long? AdvertisementVariantId,
    int? Quantity
) : BaseFilter();