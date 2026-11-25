using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Dictionaries.Attributes;
public sealed record AttributeDictionaryCacheContext
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
