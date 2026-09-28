namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Update;
public sealed record AttributeDictionaryValueUpdateRequest
(
    long? DictionaryId,
    string? Code,
    string? Value
);