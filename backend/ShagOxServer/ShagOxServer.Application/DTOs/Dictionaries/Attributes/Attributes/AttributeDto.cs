using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
public sealed record AttributeDto
(
    long AttributeId,
    string AttributeKey,
    IReadOnlyList<AttributeDictionaryValueDto> Values
);
