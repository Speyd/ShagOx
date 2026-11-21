using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank.Models;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank;
public sealed class MonoWebhookDeserializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Result<WebhookDto> Deserialize(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return Result<WebhookDto>.Fail(PaymentResources.WebhookInvalidPayload);
        }

        try
        {
            var monoWebhook = JsonSerializer.Deserialize<MonoWebhook>(body, Options);

            var webhook = new WebhookDto(
                monoWebhook!.InvoiceId,
                monoWebhook.Status,
                monoWebhook.Amount,
                monoWebhook.Ccy,
                monoWebhook.FinalAmount,
                monoWebhook.Reference,
                monoWebhook.ModifiedDate)
            {
                EnumStatus = MonoBankStatusMapper.Map(monoWebhook.Status)
            };

            return webhook is null
                ? Result<WebhookDto>.Fail(PaymentResources.WebhookInvalidPayload)
                : Result<WebhookDto>.Success(webhook);
        }
        catch (JsonException)
        {
            return Result<WebhookDto>.Fail(PaymentResources.WebhookInvalidPayload);
        }
    }
}
