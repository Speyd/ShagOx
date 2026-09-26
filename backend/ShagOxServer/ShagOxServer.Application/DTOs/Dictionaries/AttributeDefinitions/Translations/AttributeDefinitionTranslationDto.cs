using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations;
public sealed record AttributeDefinitionTranslationDto
(
    long Id,
    string Name
) : BaseDto(Id);