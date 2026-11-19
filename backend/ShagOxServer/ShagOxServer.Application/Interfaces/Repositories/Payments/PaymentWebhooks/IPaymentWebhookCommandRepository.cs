namespace ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
public interface IPaymentWebhookCommandRepository
{
    Task MarkAsProcessedAsync(
        long webhookId,
        CancellationToken cancellationToken = default);
}
