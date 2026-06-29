using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
public sealed record AttributeDefinitionUpdateRequest
(
    int? CategoryId,
    string? Key,
    AttributeType? Type,
    bool? Required,
    int? Min,
    int? Max
);