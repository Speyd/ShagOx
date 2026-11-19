namespace ShagOxServer.Application.DTOs.Payment;
public sealed record PaymentRequest
(
    decimal Amount,
    long CurrencyId,
    int Ccy,
    int DecimalPlaces
);
