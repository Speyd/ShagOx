namespace ShagOxServer.Application.DTOs.Orders.Core.Status;
public sealed record OrderShipRequest
(
    // Номер ТТН. Обов'язковий для доставки, ігнорується для самовивозу.
    string? TrackingNumber
);
