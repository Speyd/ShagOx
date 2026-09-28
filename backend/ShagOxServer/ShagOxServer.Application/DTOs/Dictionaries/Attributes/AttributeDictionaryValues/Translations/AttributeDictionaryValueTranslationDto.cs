using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
public sealed record AttributeDictionaryValueTranslationDto
(
    long Id,
    string Name
) : BaseDto(Id);