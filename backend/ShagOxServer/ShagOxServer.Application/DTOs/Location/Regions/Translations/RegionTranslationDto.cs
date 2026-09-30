using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Location.Regions.Translations;
public sealed record RegionTranslationDto
(
    long Id,
    string Name
) : BaseDto(Id);