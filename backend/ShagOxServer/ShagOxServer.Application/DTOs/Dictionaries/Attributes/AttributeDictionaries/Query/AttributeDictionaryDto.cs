using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
public sealed record AttributeDictionaryDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    List<long> AttributeIds,
    List<long> ValueIds
) : BaseDto(Id, EntityStatus);
