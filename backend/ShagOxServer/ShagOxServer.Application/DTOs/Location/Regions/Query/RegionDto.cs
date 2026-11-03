using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Location.Regions.Query;
public sealed record RegionDto
(
    long Id,
    EntityStatus EntityStatus,
    string? Code,
    string? Label
) : BaseDto(Id, EntityStatus);
