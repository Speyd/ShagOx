using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
public sealed record StatusTranslationDto
(
    long Id,
    string Name,
    string Description
) : BaseDto(Id);