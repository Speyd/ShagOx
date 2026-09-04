namespace ShagOxServer.Domain.Filters.Location.Regions.Translations;
public sealed record RegionTranslationSearchFilter
(
    string? RegionCode,
    string? Language,
    string? Name
);