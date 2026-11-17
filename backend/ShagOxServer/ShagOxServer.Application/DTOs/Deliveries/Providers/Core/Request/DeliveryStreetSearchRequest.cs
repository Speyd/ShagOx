namespace ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
public sealed record DeliveryStreetSearchRequest
(
    string CityExternalId,
    string Search
);
