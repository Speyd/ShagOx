using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Cache;
public sealed record AttributeDictionaryCacheInfo
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
