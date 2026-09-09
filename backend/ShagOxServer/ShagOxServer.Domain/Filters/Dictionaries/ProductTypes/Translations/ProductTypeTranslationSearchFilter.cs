namespace ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
public sealed record ProductTypeTranslationSearchFilter
(
    string? ProductTypeCode,
    string? Language,
    string? Name
);