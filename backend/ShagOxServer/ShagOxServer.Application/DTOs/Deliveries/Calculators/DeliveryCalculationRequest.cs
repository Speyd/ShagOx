using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Application.DTOs.Deliveries.Calculators;
public sealed record DeliveryCalculationRequest
(
    DeliveryType DeliveryType,
    DeliveryPointType? DeliveryPointType,
    string? DeliveryPointExternalId,
    string? DeliveryAddress,
    string SenderCityExternalId,
    decimal Weight,
    decimal DeclaredValue
);
