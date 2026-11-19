namespace ShagOxServer.Application.DTOs.Deliveries;
public sealed record AmountConversionResult
(
    decimal? ConvertedAmount,
    decimal Coefficient
);
