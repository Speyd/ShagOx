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
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank;
public class MonoBankPaymentProvider
    : IPaymentProvider
{
    private readonly IPaymentWebhookProcessor _webhookProcessor;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly MonoWebhookDeserializer _deserializer;
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
        HttpClient httpClient,
        IOptions<MonoBankOptions> options,
        IUnitOfWork unitOfWork)
    {
        _webhookProcessor = webhookProcessor;
        _paymentRepository = paymentRepository;
        _deserializer = deserializer;
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
            ExpiresAt = DateTime.UtcNow + _options.PaymentExpiresAt,
            Status = PaymentStatus.Pending
        };

        _paymentRepository.Add(payment);

        await _unitOfWork.SaveChangesAsync();

        return Result<PaymentResponse>.Success(responce);
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
