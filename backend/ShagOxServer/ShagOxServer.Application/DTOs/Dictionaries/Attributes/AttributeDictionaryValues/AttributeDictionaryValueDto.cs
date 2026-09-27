using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues;
public sealed record AttributeDictionaryValueDto
(
    long Id,
    long DictionaryId,
    string Code,
    string? Value
) : BaseDto(Id);