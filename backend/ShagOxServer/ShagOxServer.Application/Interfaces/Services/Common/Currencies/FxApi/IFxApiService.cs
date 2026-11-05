using ShagOxServer.Application.DTOs.Common.Currencies.Fx;

namespace ShagOxServer.Application.Interfaces.Services.Common.Currencies.FxApi;
public interface IFxApiService
{
    Task<FxApiResponse> GetLatestRatesAsync(
        string baseCurrency = "EUR",
        CancellationToken cancellationToken = default);
}
