using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Update;
public sealed record DeliveryProviderTranslationUpdateRequest
(
    long? TranslatableId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(TranslatableId, Language);
