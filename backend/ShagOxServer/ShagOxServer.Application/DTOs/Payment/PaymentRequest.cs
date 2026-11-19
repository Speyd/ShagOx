namespace ShagOxServer.Application.DTOs.Payment;
public sealed record PaymentRequest
(
    decimal Amount,
    int Ccy,
    int DecimalPlaces
);
