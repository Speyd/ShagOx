using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Location.Regions.Query;

namespace ShagOxServer.Application.DTOs.Location.Cities.Query;
public sealed record CityDto
(
    long Id,
    string Code,
    RegionDto Region,
    string? Lable
) : BaseDto(Id);