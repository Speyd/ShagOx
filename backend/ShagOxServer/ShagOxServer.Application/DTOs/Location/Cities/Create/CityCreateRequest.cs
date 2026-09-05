namespace ShagOxServer.Application.DTOs.Location.Cities.Create;
public sealed record CityCreateRequest
(
    string Code,
    int RegionId
);