namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query.Facets;
public sealed record AdvertisementFacetsDto
(
    decimal? MinPrice,
    decimal? MaxPrice,
    List<AttributeFacetDto> Attributes
);
