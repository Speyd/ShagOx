using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Location.Regions;
public sealed record RegionDto
(
    long Id,
    string Code,
    string? Lable
) : BaseDto(Id);