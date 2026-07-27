namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record AdvertisementSearchFilter
(
    string? Title,
    string? Description,
    int? CategoryId
);