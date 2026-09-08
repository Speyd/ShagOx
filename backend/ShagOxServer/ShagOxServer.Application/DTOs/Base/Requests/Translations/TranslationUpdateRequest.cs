namespace ShagOxServer.Application.DTOs.Base.Requests.Translations;
public abstract record TranslationUpdateRequest
(
    int? TranslatableId,
    string? Language
);