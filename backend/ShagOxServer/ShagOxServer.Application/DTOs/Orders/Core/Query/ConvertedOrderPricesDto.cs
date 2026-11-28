using ShagOxServer.Application.DTOs.Specification.Currencies.Query;

namespace ShagOxServer.Application.DTOs.Orders.Core.Query;
public sealed record ConvertedOrderPricesDto
(
    CurrencyConvertDto SubtotalConversion,
    CurrencyConvertDto DeliveryPriceConversion,
    CurrencyConvertDto TotalPriceConversion
);
