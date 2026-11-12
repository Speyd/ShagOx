using ShagOxServer.Domain.Filters.LineItems.Core;

namespace ShagOxServer.Domain.Filters.Orders.OrderItems;
public sealed record OrderItemSearchFilter
(
    long? OrderId,
    long? AdvertisementId,
    long? AdvertisementVariantId,
    int? Quantity,
    decimal? TotalPrice
) : LineItemSearchFilter(
        OrderId,
        AdvertisementId,
        AdvertisementVariantId,
        Quantity);
