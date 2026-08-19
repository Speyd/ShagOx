namespace ShagOxServer.Domain.Filters.Advertisements.Translations;
public sealed record StatusTranslationSearchFilter
(
    string? StatusCode,
    string? Language,
    string? Name,
    string? Description
);