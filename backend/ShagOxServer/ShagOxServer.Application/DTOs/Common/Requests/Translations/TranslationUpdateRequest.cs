namespace ShagOxServer.Application.DTOs.Common.Requests.Translations;
public abstract record TranslationUpdateRequest
(
    int? TranslatableId,
    string? Language
);