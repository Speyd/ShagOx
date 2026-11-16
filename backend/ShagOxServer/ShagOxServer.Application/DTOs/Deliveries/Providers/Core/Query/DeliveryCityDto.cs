namespace ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
public sealed record DeliveryCityDto
(
    string ExternalId,
    string Name,
    string? Area
);
