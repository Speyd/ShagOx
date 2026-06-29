namespace ShagOxServer.Application.DTOs.Location.Cities.Update;
public sealed record CityUpdateRequest
(
    string? Name,
    int? RegionId
);
