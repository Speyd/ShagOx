using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;


namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.PaymentWebhooks;
public class PaymentWebhookQueryRepository
    : QueryRepository<PaymentWebhook>,
      IPaymentWebhookQueryRepository
{
    public PaymentWebhookQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    public async Task<PaymentWebhook?> GetByInvoiceIdAsync(
        string invoiceId,
        PaymentStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        return await _db.PaymentWebhooks
            .FirstOrDefaultAsync(
                x => x.InvoiceId == invoiceId &&
                (status != null && x.Status == status) ||
                status == null);
    }

    public async Task<List<PaymentWebhook>> GetByInvoiceIdAsync(
        string invoiceId,
        CancellationToken cancellationToken = default,
        params PaymentStatus[] statuses)
    {
        return await _db.PaymentWebhooks
            .Where(x => x.InvoiceId == invoiceId &&
                statuses.Contains(x.Status))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<PaymentWebhook>> GetUnprocessedAsync(
        CancellationToken cancellationToken = default,
        int countWebhook = 100,
        EntityStatus? status = null)
    {
        return await _db.PaymentWebhooks
            .WithStatusFilter(status)
            .Where(x => !x.IsProcessed)
            .OrderBy(x => x.ReceivedAt)
            .Take(countWebhook)
            .ToListAsync(cancellationToken);
    }

}
