namespace ShagOxServer.Domain.Filters.Location.Cities;
public sealed record CitySearchFilter
(
    string? Code,
    int? RegionId
);