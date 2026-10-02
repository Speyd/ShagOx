namespace ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
public sealed record AttributeDictionarySearchFilter
(
    string? Code
) : BaseFilter();