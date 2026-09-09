namespace ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;
public sealed record CategoryTranslationSearchFilter
(
    string? CategoryCode,
    string? Language,
    string? Name
);