using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Deliveries.Providers;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Responses;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;
public class NovaPoshtaApiClient
{
    private readonly HttpClient _httpClient;
    private readonly NovaPoshtaOptions _options;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public NovaPoshtaApiClient(
        HttpClient httpClient,
        IOptions<NovaPoshtaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<NovaPoshtaResponse<T>?> SendAsync<T>(
        string modelName,
        string calledMethod,
        object methodProperties,
        CancellationToken cancellationToken = default)
    {
        var body = new
        {
            apiKey = _options.ApiKey,
            modelName,
            calledMethod,
            methodProperties
        };

        var json = JsonSerializer.Serialize(body);

        using var response = await _httpClient.PostAsync(
            "",
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"),
            cancellationToken);

        var rawResponse = await response.Content.ReadAsStringAsync(
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Nova Poshta returned {(int)response.StatusCode}: {rawResponse}");
        }

        var result = JsonSerializer.Deserialize<NovaPoshtaResponse<T>>(
            rawResponse,
            JsonOptions);

        if (result?.Success != true)
        {
            throw new InvalidOperationException(
                "Nova Poshta API error: " +
                string.Join(", ", result?.Errors ?? []));
        }

        return result;
    }
}
