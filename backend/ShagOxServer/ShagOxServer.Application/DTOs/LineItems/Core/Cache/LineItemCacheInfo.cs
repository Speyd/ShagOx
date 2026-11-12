using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.LineItems.Core.Cache;
public sealed record LineItemCacheInfo
(
    long Id,
    long ParentId,
    long AdvertId,
    long? AdvertVariantId,
    EntityStatus Status = EntityStatus.Active
);
