using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
public sealed record AttributeDefinitionTranslationDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name
) : BaseDto(Id, EntityStatus);
