using ShagOxServer.Application.DTOs.Common.Currencies.Fx;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies.FxApi;
using System.Net.Http.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Currencies.FxApi;
public class FxApiService : IFxApiService
{
    private readonly HttpClient _httpClient;

    public FxApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FxApiResponse> GetLatestRatesAsync(
        string baseCurrency = "EUR",
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"latest?base_currency={baseCurrency}",
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<FxApiResponse>(
                cancellationToken: cancellationToken);

        return result ?? throw new InvalidOperationException(
            "fxapi returned an empty response.");
    }
}
