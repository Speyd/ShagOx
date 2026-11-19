using ShagOxServer.Application.DTOs.Payment.Webhooks;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Payments;
public interface IPaymentWebhookProcessor
{
    Task<Result<bool>> ReceiveAsync(
        MonoWebhook webhook,
        string payload,
        CancellationToken cancellationToken = default);

    Task ProcessPendingAsync(
        CancellationToken cancellationToken = default);

    Task ProcessPendingAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
