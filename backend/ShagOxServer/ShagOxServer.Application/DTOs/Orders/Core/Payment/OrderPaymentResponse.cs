namespace ShagOxServer.Application.DTOs.Orders.Core.Payment;
public sealed record OrderPaymentResponse
(
    string PaymentUrl,
    DateTime ExpiresAt
);
