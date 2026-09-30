using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Update;
public sealed record AttributeDefinitionUpdateRequest
(
    long? CategoryId,
    string? Key,
    AttributeType? Type,
    bool? Required,
    int? Min,
    int? Max,
    bool? IsVariant,
    bool? Multiple
);