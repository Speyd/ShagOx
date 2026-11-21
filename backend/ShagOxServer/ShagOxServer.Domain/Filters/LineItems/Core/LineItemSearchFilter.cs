namespace ShagOxServer.Domain.Filters.LineItems.Core;
public record LineItemSearchFilter
(
    long? ParentId,
    long? AdvertisementId,
    long? AdvertisementVariantId,
    int? Quantity
) : BaseFilter();
