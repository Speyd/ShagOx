using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
public sealed record DeliveryPointSearchRequest
(
    string CityExternalId,
    string? Search,
    DeliveryPointType DeliveryPointType
);
