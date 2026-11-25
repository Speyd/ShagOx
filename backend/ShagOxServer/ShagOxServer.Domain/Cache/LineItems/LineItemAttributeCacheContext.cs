using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.LineItems;
public sealed record LineItemAttributeCacheContext
(
    long Id,
    long CategoryId,
    long AttributeDefinitionId,
    EntityStatus Status = EntityStatus.Active
);
