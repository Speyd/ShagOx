using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
public sealed record StatusDto
(
    long Id,
    string Code,
    string? Label
) : BaseDto(Id);
