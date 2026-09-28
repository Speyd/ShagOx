using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
public sealed record CategoryTranslationDto
(
    long Id,
    string Name
) : BaseDto(Id);