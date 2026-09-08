using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses;
public sealed record StatusDto
(
    int Id,
    string Code
) : BaseDto(Id);