using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Query;
public sealed record ConditionDto
(
    long Id,
    string Code,
    string? Lable
) : BaseDto(Id);