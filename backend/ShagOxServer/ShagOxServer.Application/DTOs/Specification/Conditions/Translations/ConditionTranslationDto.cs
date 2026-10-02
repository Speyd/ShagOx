using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Translations;
public sealed record ConditionTranslationDto
(
    long Id,
    string Name
) : BaseDto(Id);