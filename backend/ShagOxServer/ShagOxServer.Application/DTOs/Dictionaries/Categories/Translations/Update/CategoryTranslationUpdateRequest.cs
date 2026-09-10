using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Update;
public sealed record CategoryTranslationUpdateRequest
(
    int? TranslatableId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(TranslatableId, Language);