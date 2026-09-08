
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Location.Regions;
public sealed record RegionDto
(
    int Id,
    string Code
) : BaseDto(Id);