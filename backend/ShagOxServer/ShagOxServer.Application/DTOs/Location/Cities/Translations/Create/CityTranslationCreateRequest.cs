using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Location.Cities.Translations.Create;
public sealed record CityTranslationCreateRequest
(
    long TranslatableId,
    string Language,
    string Name
) : TranslationCreateRequest(TranslatableId, Language);