using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
public sealed record CategoryCacheInfo
(
    long Id,
    long ProductTypeId,
    EntityStatus Status = EntityStatus.Active
);
