using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Catalog;
public sealed record AdvertisementVariantCatalogDto
(
    long Id,
    decimal Price,
    decimal? PreviousPrice,
    int Stock,
    List<AttributeDto> Attributes
);
