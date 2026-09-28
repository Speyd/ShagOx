namespace ShagOxServer.Application.DTOs.Base.Requests.Translations;
public abstract record TranslationUpdateRequest
(
    long? TranslatableId,
    string? Language
);