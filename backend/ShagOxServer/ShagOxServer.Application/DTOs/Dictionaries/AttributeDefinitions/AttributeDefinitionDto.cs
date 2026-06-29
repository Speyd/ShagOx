using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;

public sealed record AttributeDefinitionDto
(
    int Id,
    int CategoryId,
    string CategoryName,
    string Key,
    AttributeType Type,
    bool Required,
    int? Min,
    int? Max
);