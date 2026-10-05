using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
public sealed record AttributeDictionaryDto
(
    long Id,
    string Code,
    List<long> AttributeIds,
    List<long> ValueIds
) : BaseDto(Id);