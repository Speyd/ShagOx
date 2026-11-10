using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Catalog;
public sealed record AdvertisementVariantCatalogDto
(
    int Stock,
    List<AttributeDto> Attributes
);
