using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Create;
public sealed record DeliveryProviderTranslationCreateRequest
(
    long TranslatableId,
    string Language,
    string Name
) : TranslationCreateRequest(TranslatableId, Language);
