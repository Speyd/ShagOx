using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
public sealed record AttributeDefinitionCreateRequest
(
    int CategoryId,
    string Key,
    AttributeType Type,
    bool Required,
    int? Min,
    int? Max
);