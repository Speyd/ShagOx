using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Query;
public sealed record StatusTranslationDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name,
    string Description
) : BaseDto(Id, EntityStatus);
