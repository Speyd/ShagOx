using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Payments.Providers;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Enum;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank;
public class MonoBankPaymentProvider
    : IPaymentProvider
{
    private readonly IPaymentWebhookProcessor _webhookProcessor;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly MonoWebhookDeserializer _deserializer;
    private readonly MonoBankSignatureVerifier _signatureVerifier;
    private readonly HttpClient _httpClient;
    private readonly MonoBankOptions _options;
    private readonly IUnitOfWork _unitOfWork;


    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };


    public MonoBankPaymentProvider(
        IPaymentWebhookProcessor webhookProcessor,
        IRepository<Payment> paymentRepository,
        MonoWebhookDeserializer deserializer,
        MonoBankSignatureVerifier signatureVerifier,
        HttpClient httpClient,
        IOptions<MonoBankOptions> options,
        IUnitOfWork unitOfWork)
    {
        _webhookProcessor = webhookProcessor;
        _paymentRepository = paymentRepository;
        _deserializer = deserializer;
        _signatureVerifier = signatureVerifier;
        _httpClient = httpClient;
        _options = options.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaymentResponse>> PaymentRequest(
        PaymentRequest request, 
        CancellationToken cancellationToken = default)
    {
        var trueAmount = MoneyConverter.ToMinorUnits(
            request.Amount, 
            request.DecimalPlaces);

        var expiresIn = _options.PaymentExpiresAt;

        // Монобанк сам закриває рахунок після validity (за замовчуванням
        // 24 год). Синхронізуємо з нашим терміном, щоб після закінчення
        // посилання оплатити старий рахунок було вже неможливо.
        var data = new
        {
            amount = trueAmount,
            ccy = request.Ccy,
            redirectUrl = BuildRedirectUrl(request.OrderId),
            webHookUrl = _options.WebHookUrl,
            validity = (long)expiresIn.TotalSeconds
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
            return Result<PaymentResponse>.Fail(
               PaymentResources.PaymentProviderInvalidResponse);
        }

        Console.WriteLine(responce.InvoiceId);

        var payment = new Payment
        {
            OrderId = request.OrderId,
            InvoiceId = responce.InvoiceId,
            PaymentUrl = responce.PaymentUrl,
            Amount = request.Amount,
            ExpiresAt = DateTime.UtcNow + expiresIn,
            Status = PaymentStatus.Pending
        };

        _paymentRepository.Add(payment);

        await _unitOfWork.SaveChangesAsync();

        return Result<PaymentResponse>.Success(responce);
    }

    /// <summary>
    /// RedirectUrl може містити плейсхолдер {orderId}, наприклад
    /// https://site/orders/{orderId} — тоді після оплати користувач
    /// повертається на сторінку свого замовлення.
    /// </summary>
    private string BuildRedirectUrl(long orderId)
    {
        return _options.RedirectUrl.Replace(
            "{orderId}",
            orderId.ToString(),
            StringComparison.OrdinalIgnoreCase);
    }

    public async Task<Result<bool>> WebHook(
        HttpRequest request,
        CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(request.Body);

        var body = await reader.ReadToEndAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(body))
        {
            return Result<bool>.Fail(
                PaymentResources.WebhookEmptyBody);
        }

        // Приймаємо лише вебхуки, підписані банком.
        var signature = request.Headers["X-Sign"].FirstOrDefault();

        var isSigned = await _signatureVerifier.VerifyAsync(
            body,
            signature,
            cancellationToken);

        if (!isSigned)
        {
            return Result<bool>.Fail(
                PaymentResources.WebhookInvalidPayload,
                ResultErrorType.Unauthorized);
        }

        var payment = _deserializer.Deserialize(body);

        if (!payment.IsSuccess || payment.Value is null)
        {
            return Result<bool>.Fail(
                payment.Error);
        }

        return await _webhookProcessor.ReceiveAsync(
            payment.Value,
            body,
            cancellationToken);
    }
}
