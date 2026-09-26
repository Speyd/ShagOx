using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
public sealed record AttributeDefinitionCreateRequest
(
    long CategoryId,
    string Key,
    AttributeType Type,
    bool Required,
    decimal? Min,
    decimal? Max,
    bool IsVariant,
    bool Multiple
);