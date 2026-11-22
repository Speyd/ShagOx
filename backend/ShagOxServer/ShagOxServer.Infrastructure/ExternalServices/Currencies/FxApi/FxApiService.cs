using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Common.Options.Currencies.Fx;
using ShagOxServer.Application.DTOs.Common.Currencies.Fx;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies.FxApi;
using System.Net.Http.Json;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Currencies.FxApi;
public class FxApiService : IFxApiService
{
    private readonly HttpClient _httpClient;
    private readonly FxApiOptions _options;
    private readonly ILogger<FxApiService> _logger;


    public FxApiService(
        HttpClient httpClient,
        FxApiOptions options,
        ILogger<FxApiService> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;
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

        if(result is not null)
        {
            await SaveToJsonAsync(
                result,
                cancellationToken);

            return result;
        }
        else
        {
            _logger.LogWarning("FX API returned an empty response." +
                " Falling back to cached currency rates.");

            var resultFromJson = await ReadFromJsonAsync(
                cancellationToken);

            return resultFromJson ?? throw new InvalidOperationException(
                "fxapi returned an empty response.");
        }
    }

    private async Task SaveToJsonAsync(
        FxApiResponse result,
        CancellationToken cancellationToken)
    {
        var directory = Path.Combine(
            AppContext.BaseDirectory,
            _options.RatesFilePath);

        Directory.CreateDirectory(directory);

        var filePath = Path.Combine(
            directory,
            $"{_options.RatesFileName}.json");

        await using var stream = File.Create(filePath);

        await JsonSerializer.SerializeAsync(
            stream,
            result,
            new JsonSerializerOptions
            {
                WriteIndented = true
            },
            cancellationToken);
    }

    private async Task<FxApiResponse?> ReadFromJsonAsync(
        CancellationToken cancellationToken)
    {
        var directory = Path.Combine(
            AppContext.BaseDirectory,
            _options.RatesFilePath);
        
        var filePath = Path.Combine(
            directory,
            $"{_options.RatesFileName}.json");

        await using var stream = File.Open(filePath, FileMode.Open);

        return await JsonSerializer.DeserializeAsync<FxApiResponse>(
            stream,
            cancellationToken: cancellationToken);
    }
}
