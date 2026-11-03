using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Location.Regions.Translations.Query;
public sealed record RegionTranslationDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name
) : BaseDto(Id, EntityStatus);
