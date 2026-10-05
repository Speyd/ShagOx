using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public sealed record AttributeDictionaryValueDto
(
    long Id,
    long DictionaryId,
    string Code,
    string? Value,
    string? Label
) : BaseDto(Id);