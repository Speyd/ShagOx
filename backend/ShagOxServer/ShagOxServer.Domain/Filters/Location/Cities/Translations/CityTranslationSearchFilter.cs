namespace ShagOxServer.Domain.Filters.Location.Cities.Translations;
public sealed record CityTranslationSearchFilter
(
    string? CityCode,
    string? Language,
    string? Name
);