namespace ShagOxServer.Domain.Filters.Deliveries;
public sealed record DeliveryProviderSearchFilter
(
    string? Code
) : BaseFilter();
