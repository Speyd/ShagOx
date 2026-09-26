using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Location.Regions;

namespace ShagOxServer.Application.DTOs.Location.Cities;
public sealed record CityDto
(
    long Id,
    string Code,
    RegionDto Region
) : BaseDto(Id);