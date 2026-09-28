namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Create;
public sealed record AttributeDictionaryValueCreateRequest
(
    long DictionaryId,
    string Code,
    string? Value
);