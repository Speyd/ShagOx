using ShagOxServer.Domain.Filters.LineItems.Core;

namespace ShagOxServer.Domain.Filters.Baskets.BasketItems;
public sealed record BasketItemSearchFilter
(
    long? BasketId,
    long? AdvertisementId,
    long? AdvertisementVariantId,
    int? Quantity
) : LineItemSearchFilter(
        BasketId, 
        AdvertisementId, 
        AdvertisementVariantId,
        Quantity);
