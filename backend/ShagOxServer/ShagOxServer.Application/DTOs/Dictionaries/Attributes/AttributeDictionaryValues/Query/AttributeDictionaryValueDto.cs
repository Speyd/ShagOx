using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public sealed record AttributeDictionaryValueDto
(
    long Id,
    EntityStatus EntityStatus,
    long DictionaryId,
    string Code,
    string? Value,
    string? Label
) : BaseDto(Id, EntityStatus);
