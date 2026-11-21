namespace ShagOxServer.Application.DTOs.Payment;
public sealed record PaymentRequest
(
    decimal Amount,
    long OrderId,
    int Ccy,
    int DecimalPlaces
);
