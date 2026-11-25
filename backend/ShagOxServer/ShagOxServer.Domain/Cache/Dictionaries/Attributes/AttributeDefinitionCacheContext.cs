using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Dictionaries.Attributes;
public sealed record AttributeDefinitionCacheContext
(
    long Id,
    long CategoryId,
    EntityStatus Status = EntityStatus.Active
);
