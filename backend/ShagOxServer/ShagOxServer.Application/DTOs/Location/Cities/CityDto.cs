
namespace ShagOxServer.Application.DTOs.Location.Cities;
public sealed record CityDto
(
    int Id,
    string Name,
    int RegionId,
    string NameRegion
);
