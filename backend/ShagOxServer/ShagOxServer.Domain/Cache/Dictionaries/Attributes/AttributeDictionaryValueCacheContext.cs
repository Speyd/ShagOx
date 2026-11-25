using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Dictionaries.Attributes;
public sealed record AttributeDictionaryValueCacheContext
(
    long Id,
    long DictionaryId,
    EntityStatus Status = EntityStatus.Active
);
