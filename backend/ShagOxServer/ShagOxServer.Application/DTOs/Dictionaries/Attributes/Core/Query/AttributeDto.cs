using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
public sealed record AttributeDto
(
    AttributeDefinitionDto? Attribute,
    IReadOnlyList<AttributeDictionaryValueDto>? Values
);
