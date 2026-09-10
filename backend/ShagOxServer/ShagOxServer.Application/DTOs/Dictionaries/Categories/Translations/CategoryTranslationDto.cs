using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
public sealed record CategoryTranslationDto
(
    int Id,
    string Name
) : BaseDto(Id);