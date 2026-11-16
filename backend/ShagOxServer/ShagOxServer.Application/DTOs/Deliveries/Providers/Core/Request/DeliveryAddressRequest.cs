namespace ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
public sealed record DeliveryAddressRequest
(
    string StreetExternalId,
    string BuildingNumber,
    string? FlatNumber
);
