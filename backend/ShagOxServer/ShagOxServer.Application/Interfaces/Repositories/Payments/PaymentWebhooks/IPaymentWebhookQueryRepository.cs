using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Payments;

namespace ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
public interface IPaymentWebhookQueryRepository
{
    Task<IReadOnlyList<PaymentWebhook>> GetUnprocessedAsync(
        CancellationToken cancellationToken = default,
        int countWebhook = 100,
        EntityStatus? status = null);

    Task<PaymentWebhook?> GetByInvoiceIdAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
