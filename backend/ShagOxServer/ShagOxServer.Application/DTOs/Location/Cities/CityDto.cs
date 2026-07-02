using ShagOxServer.Application.DTOs.Location.Regions;

namespace ShagOxServer.Application.DTOs.Location.Cities;
public sealed record CityDto
(
    int Id,
    string Name,
    RegionDto Region
);
