using ShagOxServer.Application.DTOs.Specification.Currencies.Query;

namespace ShagOxServer.Application.DTOs.Common.Currencies;

public sealed record ConvertedPricesDto
(
    CurrencyConvertDto Price,
    CurrencyConvertDto PreviousPrice
);
