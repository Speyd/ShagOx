namespace ShagOxServer.Domain.Filters.Deliveries.Translations;
public sealed record DeliveryProviderTranslationSearchFilter
(
    string? DeliveryProviderCode,
    string? Language,
    string? Name
) : BaseFilter();
