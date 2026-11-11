namespace ShagOxServer.Domain.Filters.Deliveries.DeliveryProviders;
public sealed record DeliveryProviderSearchFilter
(
    string? Code
) : BaseFilter();
