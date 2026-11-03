using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Query;
public sealed record ConditionDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    string? Label
) : BaseDto(Id, EntityStatus);
