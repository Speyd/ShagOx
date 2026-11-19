namespace ShagOxServer.Application.DTOs.Orders.Core.Create;
public record OrderCreateResponse
(
    long Id,
    string? PaymentUrl,
    DateTime CreatedAt
);
