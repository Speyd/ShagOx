namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record AdvertisementSearchFilter
(
    string? Title,
    int? Stock,
    string? Description,
    int? CategoryId
);