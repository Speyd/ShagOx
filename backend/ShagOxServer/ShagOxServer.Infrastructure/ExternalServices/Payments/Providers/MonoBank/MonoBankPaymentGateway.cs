using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Payments.Providers;
using ShagOxServer.Application.Interfaces.Providers.Payment;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank;
public class MonoBankPaymentGateway
    : IPaymentGateway
{
    private readonly HttpClient _httpClient;
    private readonly MonoBankOptions _options;
    private readonly ILogger<MonoBankPaymentGateway> _logger;


    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };


    public MonoBankPaymentGateway(
        HttpClient httpClient,
        IOptions<MonoBankOptions> options,
        ILogger<MonoBankPaymentGateway> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }


    public async Task<Result<bool>> RefundAsync(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        var (success, body) = await SendAsync(
            "cancel",
            new { invoiceId },
            cancellationToken);

        if (!success)
        {
            _logger.LogError(
                "Monobank refund failed. InvoiceId: {InvoiceId}, Response: {Response}",
                invoiceId,
                body);

            return Result<bool>.Fail(PaymentResources.PaymentRefundFailed);
        }

        var status = TryReadStatus(body);

        if (string.Equals(status, "failure", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogError(
                "Monobank rejected refund. InvoiceId: {InvoiceId}, Response: {Response}",
                invoiceId,
                body);

            return Result<bool>.Fail(PaymentResources.PaymentRefundFailed);
        }

        _logger.LogInformation(
            "Monobank refund accepted. InvoiceId: {InvoiceId}, Status: {Status}",
            invoiceId,
            status);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> CancelInvoiceAsync(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        var (success, body) = await SendAsync(
            "remove",
            new { invoiceId },
            cancellationToken);

        if (!success)
        {
            _logger.LogWarning(
                "Monobank invoice remove failed. InvoiceId: {InvoiceId}, Response: {Response}",
                invoiceId,
                body);

            return Result<bool>.Fail(PaymentResources.PaymentStatusUpdateFailed);
        }

        return Result<bool>.Success(true);
    }

    private async Task<(bool Success, string Body)> SendAsync(
        string action,
        object payload,
        CancellationToken cancellationToken)
    {
        var url = new Uri(new Uri(_options.InvoiceCreateUrl), action);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);

        request.Headers.Add("X-Token", _options.ApiKey);
        request.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        try
        {
            using var response = await _httpClient.SendAsync(
                request,
                cancellationToken);

            var body = await response.Content
                .ReadAsStringAsync(cancellationToken);

            return (response.IsSuccessStatusCode, body);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Monobank request failed. Action: {Action}", action);
            return (false, ex.Message);
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex, "Monobank request timed out. Action: {Action}", action);
            return (false, "timeout");
        }
    }

    private static string? TryReadStatus(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
            return null;

        try
        {
            var response = JsonSerializer.Deserialize<StatusResponse>(body, JsonOptions);
            return response?.Status;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private sealed record StatusResponse(string? Status);
}
