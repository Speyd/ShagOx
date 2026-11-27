namespace ShagOxServer.Application.DTOs.Orders;
public sealed record OrderPaymentResponse
(
    string PaymentUrl,
    DateTime ExpiresAt
);
