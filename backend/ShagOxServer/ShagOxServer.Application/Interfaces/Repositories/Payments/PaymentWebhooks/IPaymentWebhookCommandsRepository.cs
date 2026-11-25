using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
public interface IPaymentWebhookCommandsRepository
{
    Task MarkAsProcessedAsync(
        long webhookId,
        CancellationToken cancellationToken = default);

    Task UpdateStatusAsync(
        long webhookId,
        PaymentStatus status,
        CancellationToken cancellationToken = default);
}
