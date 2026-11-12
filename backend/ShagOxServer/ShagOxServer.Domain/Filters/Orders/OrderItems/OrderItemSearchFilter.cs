namespace ShagOxServer.Domain.Filters.Orders.OrderItems;
public sealed record OrderItemSearchFilter
(
    long? OrderId,
    long? AdvertisementId,
    long? AdvertisementVariantId,
    int? Quantity,
    decimal? TotalPrice
) : BaseFilter();
