using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
public sealed record AttributeDictionaryValueCacheInfo
(
    long Id,
    long DictionaryId,
    EntityStatus Status = EntityStatus.Active
);
