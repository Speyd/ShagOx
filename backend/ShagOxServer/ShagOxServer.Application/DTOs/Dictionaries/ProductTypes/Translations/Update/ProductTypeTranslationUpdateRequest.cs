using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Update;
public sealed record ProductTypeTranslationUpdateRequest
(
    int? TranslatableId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(TranslatableId, Language);