using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;

public sealed record DeliveryCalculationRequest
(
    DeliveryType DeliveryType,
    DeliveryPointType? DeliveryPointType,
    string RecipientCityExternalId,
    string? DeliveryPointExternalId,
    decimal Weight,
    decimal DeclaredValue,
    int SeatsAmount = 1
);
