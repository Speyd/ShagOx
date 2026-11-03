using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Query;
public sealed record ConditionTranslationDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name
) : BaseDto(Id, EntityStatus);
