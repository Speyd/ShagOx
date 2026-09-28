using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
public sealed record AttributeDictionaryValueTranslationCreateRequest
(
    long TranslatableId,
    string Language,
    string Name
) : TranslationCreateRequest(TranslatableId, Language);