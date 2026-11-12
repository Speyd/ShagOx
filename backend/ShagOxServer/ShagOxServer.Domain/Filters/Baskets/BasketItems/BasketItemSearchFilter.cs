namespace ShagOxServer.Domain.Filters.Baskets.BasketItems;
public sealed record BasketItemSearchFilter
(
    long? BasketId,
    long? AdvertisementId,
    long? AdvertisementVariantId,
    int? Quantity
) : BaseFilter();
