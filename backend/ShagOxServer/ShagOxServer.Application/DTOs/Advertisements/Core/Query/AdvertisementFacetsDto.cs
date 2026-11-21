namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;
public sealed record AdvertisementFacetsDto
(
    decimal? MinPrice,
    decimal? MaxPrice,
    List<AttributeFacetDto> Attributes
);

public sealed record AttributeFacetDto
(
    string Key,
    string Value,
    int Count
);
