using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Payments.Providers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank;

public sealed class MonoBankSignatureVerifier
{
    private readonly HttpClient Http;

    private readonly MonoBankOptions _options;
    private readonly ILogger<MonoBankSignatureVerifier> _logger;

    private readonly SemaphoreSlim _lock = new(1, 1);
    private ECDsa? _key;
    private DateTime _lastFetchAt = DateTime.MinValue;


    public MonoBankSignatureVerifier(
        IOptions<MonoBankOptions> options,
        ILogger<MonoBankSignatureVerifier> logger)
    {
        _options = options.Value;
        _logger = logger;

        Http = new()
        {
            Timeout = options.Value.HttpClientTimeout
        };
    }


    public async Task<bool> VerifyAsync(
        string body,
        string? signatureBase64,
        CancellationToken cancellationToken = default)
    {
        if (!_options.VerifyWebhookSignature)
            return true;

        if (string.IsNullOrWhiteSpace(signatureBase64))
            return false;

        byte[] signature;

        try
        {
            signature = Convert.FromBase64String(signatureBase64);
        }
        catch (FormatException)
        {
            return false;
        }

        var data = Encoding.UTF8.GetBytes(body);

        var key = await GetKeyAsync(forceRefresh: false, cancellationToken);
        if (key is not null && Verify(key, data, signature))
            return true;

        key = await GetKeyAsync(forceRefresh: true, cancellationToken);
        return key is not null && Verify(key, data, signature);
    }

    private static bool Verify(ECDsa key, byte[] data, byte[] signature)
    {
        try
        {
            return key.VerifyData(
                data,
                signature,
                HashAlgorithmName.SHA256,
                DSASignatureFormat.Rfc3279DerSequence);
        }
        catch (CryptographicException)
        {
            return false;
        }
    }

    private async Task<ECDsa?> GetKeyAsync(
        bool forceRefresh,
        CancellationToken cancellationToken)
    {
        if (_key is not null && !forceRefresh)
            return _key;

        await _lock.WaitAsync(cancellationToken);

        try
        {
            if (_key is not null && (!forceRefresh ||
                DateTime.UtcNow - _lastFetchAt < _options.MinRefreshInterval))
            {
                return _key;
            }

            _lastFetchAt = DateTime.UtcNow;

            var key = await FetchKeyAsync(cancellationToken);
            if (key is not null)
            {
                _key?.Dispose();
                _key = key;
            }

            return _key;
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<ECDsa?> FetchKeyAsync(CancellationToken cancellationToken)
    {
        var url = new Uri(new Uri(_options.InvoiceCreateUrl), "../pubkey");

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Token", _options.ApiKey);

            using var response = await Http.SendAsync(request, cancellationToken);

            var body = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Failed to get Monobank public key. Status: {Status}, Body: {Body}",
                    response.StatusCode,
                    body);

                return null;
            }

            using var json = JsonDocument.Parse(body);
            var keyBase64 = json.RootElement.GetProperty("key").GetString();

            var pem = Encoding.UTF8.GetString(Convert.FromBase64String(keyBase64!));

            var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(pem);

            return ecdsa;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Failed to load Monobank public key.");
            return null;
        }
    }
}
