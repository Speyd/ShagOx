using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
public sealed record AttributeDefinitionDto
(
    long Id,
    long CategoryId,
    string CategoryCode,
    string Key,
    long? DictionaryId,
    AttributeType Type,
    bool Required,
    decimal? Min,
    decimal? Max,
    bool IsVariant,
    bool Multiple,
    string? Lable
) : BaseDto(Id);
