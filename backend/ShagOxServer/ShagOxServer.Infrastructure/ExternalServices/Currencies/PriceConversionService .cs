using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.ExternalServices.Currencies;
public class PriceConversionService 
    : IPriceConversionService
{
    private readonly ICurrencyProvider _currencyProvider;
    private readonly ICurrencyConverter _currencyConverter;

    public PriceConversionService(
        ICurrencyProvider currencyProvider,
        ICurrencyConverter currencyConverter)
    {
        _currencyProvider = currencyProvider;
        _currencyConverter = currencyConverter;
    }

    public async Task<CurrencyConvertDto> ConvertPriceAsync(
        decimal? price,
        Currency sourceCurrency,
        CancellationToken cancellationToken = default)
    {
        var userCurrency = await _currencyProvider
            .GetCurrencyAsync(cancellationToken);

        var convertedPrice = price;
        var coefficient = 1m;

        if (userCurrency is not null
            && _currencyConverter.TryConvert(
                price,
                sourceCurrency.Code,
                userCurrency.Code,
                out var converted,
                out var rate))
        {
            convertedPrice = converted;
            coefficient = rate;
        }
        else
        {
            userCurrency = null;
        }

        return CurrencyConvertMapper.ToDto(
            sourceCurrency,
            userCurrency,
            convertedPrice,
            coefficient);
    }

    public async Task<ConvertedPricesDto> ConvertPricesAsync(
        decimal? price,
        decimal? previousPrice,
        Currency sourceCurrency,
        CancellationToken cancellationToken = default)
    {
        var convertedPrice = await ConvertPriceAsync(
            price,
            sourceCurrency,
            cancellationToken);

        var convertedPreviousPrice = convertedPrice with
        {
            ConvertPrice = previousPrice * convertedPrice.Coefficient
        };

        return new ConvertedPricesDto(convertedPrice,
            convertedPreviousPrice);
    }
}
