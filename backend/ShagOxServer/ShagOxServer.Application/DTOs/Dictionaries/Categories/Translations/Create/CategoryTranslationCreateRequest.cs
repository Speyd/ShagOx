using ShagOxServer.Application.DTOs.Base.Requests.Translations;


namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Create;
public sealed record CategoryTranslationCreateRequest
(
    int TranslatableId,
    string Language,
    string Name
) : TranslationCreateRequest(TranslatableId, Language);