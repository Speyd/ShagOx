using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Services.Common.Currencies;
public interface IPriceConversionService
{
    Task<CurrencyConvertDto> ConvertPriceAsync(
        decimal? price,
        Currency sourceCurrency,
        CancellationToken cancellationToken = default);

    Task<ConvertedPricesDto> ConvertPricesAsync(
       decimal? price,
       decimal? previousPrice,
       Currency sourceCurrency,
       CancellationToken cancellationToken = default);

    /// <summary>
    /// Сегмент ключа кешу для відповідей з конвертованими цінами:
    /// валюта, у яку конвертуються ціни для поточного користувача.
    /// </summary>
    Task<string> GetCacheSegmentAsync(
        CancellationToken cancellationToken = default);
}
