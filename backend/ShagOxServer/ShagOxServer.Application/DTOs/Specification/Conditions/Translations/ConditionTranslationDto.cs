using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Translations;
public sealed record ConditionTranslationDto
(
    int Id,
    string Name
) : BaseDto(Id);