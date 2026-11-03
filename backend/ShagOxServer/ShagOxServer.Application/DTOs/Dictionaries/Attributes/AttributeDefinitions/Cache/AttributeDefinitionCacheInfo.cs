using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
public sealed record AttributeDefinitionCacheInfo
(
    long Id,
    long CategoryId,
    EntityStatus Status = EntityStatus.Active
);
