using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Specification.Conditions;
public sealed record ConditionDto
(
    int Id,
    string Name
) : BaseDto(Id);