namespace ShagOxServer.Application.DTOs.Location.Cities.Translations.Create;
public sealed record CityTranslationCreateRequest
(
    int CityId,
    string Language,
    string Name
);