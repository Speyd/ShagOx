using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Location.Regions.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Location.Cities.Query;
public sealed record CityDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    RegionDto Region,
    string? Label
) : BaseDto(Id, EntityStatus);
