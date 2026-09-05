namespace ShagOxServer.Application.DTOs.Location.Cities.Translations.Update;
public sealed record CityTranslationUpdateRequest
(
    int? CityId,
    string? Language,
    string? Name
);