using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.LineItems;
public sealed record LineItemCacheContext
(
    long Id,
    long ParentId,
    long AdvertId,
    long? AdvertVariantId,
    EntityStatus Status = EntityStatus.Active
);
