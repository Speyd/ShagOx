namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record AdvertisementVariantSearchFilter
(
    long? AdvertisementId,
    decimal? Price,
    int? Stock,
    List<string>? Attributes
) : BaseFilter(); 