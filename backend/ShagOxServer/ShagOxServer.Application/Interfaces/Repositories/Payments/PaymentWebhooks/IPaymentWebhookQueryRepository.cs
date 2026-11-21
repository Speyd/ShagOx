using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
public interface IPaymentWebhookQueryRepository
    : IQueryRepository<PaymentWebhook>
{
    Task<IReadOnlyList<PaymentWebhook>> GetUnprocessedAsync(
        CancellationToken cancellationToken = default,
        int countWebhook = 100,
        EntityStatus? status = null);

    Task<PaymentWebhook?> GetByInvoiceIdAsync(
        string invoiceId,
        PaymentStatus? status = null,
        CancellationToken cancellationToken = default);

    Task<List<PaymentWebhook>> GetByInvoiceIdAsync(
        string invoiceId,
        CancellationToken cancellationToken = default,
        params PaymentStatus[] statuses);
}
