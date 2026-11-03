using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
public sealed record StatusDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    string? Label
) : BaseDto(Id, EntityStatus);
