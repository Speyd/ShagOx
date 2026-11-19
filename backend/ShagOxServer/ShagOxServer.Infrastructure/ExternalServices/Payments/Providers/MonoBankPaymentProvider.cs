using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Payments.Providers;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.DTOs.Payment.Webhooks;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers;
public class MonoBankPaymentProvider
    : IPaymentProvider
{
    private readonly IPaymentWebhookProcessor _webhookProcessor;
    private readonly HttpClient _httpClient;
    private readonly MonoBankOptions _options;


    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };


    public MonoBankPaymentProvider(
        IPaymentWebhookProcessor webhookProcessor,
        HttpClient httpClient,
        IOptions<MonoBankOptions> options)
    {
        _webhookProcessor = webhookProcessor;
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<Result<PaymentResponse>> PaymentRequest(
        PaymentRequest request, 
        CancellationToken cancellationToken = default)
    {
        var trueAmount = MoneyConverter.ToMinorUnits(
            request.Amount, 
            request.DecimalPlaces);

        var data = new
        {
            amount = trueAmount,
            ccy = request.Ccy,
            redirectUrl = _options.RedirectUrl,
            webHookUrl = _options.WebHookUrl
        };

        var json = JsonSerializer.Serialize(data);

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            _options.InvoiceCreateUrl);

        httpRequest.Headers.Add("X-Token", _options.ApiKey);

        httpRequest.Content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        using var response = await _httpClient.SendAsync(
            httpRequest,
            cancellationToken);

        var result = await response.Content.ReadAsStringAsync();

        var responce = JsonSerializer
            .Deserialize<PaymentResponse>
                (result, JsonOptions);

        if(responce is null)
        {
            return Result<PaymentResponse>
                .Fail("");
        }

        return Result<PaymentResponse>.Success(responce);
    }

    public async Task<Result<bool>> WebHook(
        HttpRequest request,
        CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(request.Body);

        var body = await reader.ReadToEndAsync(cancellationToken);

        Console.WriteLine($"WEBHOOK BODY: {body}");

        if (string.IsNullOrWhiteSpace(body))
        {
            return Result<bool>.Fail("Empty webhook body");
        }

        var payment = JsonSerializer.Deserialize<MonoWebhook>(body);

        if (payment is null)
        {
            return Result<bool>.Fail("Invalid webhook");
        }

        Console.WriteLine($"Invoice: {payment.InvoiceId}");
        Console.WriteLine($"Status: {payment.Status}");

        return await _webhookProcessor.ReceiveAsync(
            payment,
            body,
            cancellationToken);
    }
}
