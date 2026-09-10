namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record AdvertisementSearchFilter
(
    string? Title,
    bool? Stock,
    string? Description,
    int? CategoryId
);