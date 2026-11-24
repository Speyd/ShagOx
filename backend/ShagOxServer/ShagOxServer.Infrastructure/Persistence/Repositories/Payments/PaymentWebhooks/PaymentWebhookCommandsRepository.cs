using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.PaymentWebhooks;
public class PaymentWebhookCommandsRepository 
    : CommandRepository<PaymentWebhook>,
    IPaymentWebhookCommandsRepository
{
    public PaymentWebhookCommandsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task MarkAsProcessedAsync(
        long webhookId,
        CancellationToken cancellationToken = default)
    {
        await _db.PaymentWebhooks
            .Where(x => x.Id == webhookId && !x.IsProcessed)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.IsProcessed, true)
                .SetProperty(x => x.ProcessedAt, DateTime.UtcNow),
                cancellationToken);
    }

    public async Task UpdateStatusAsync(
        long webhookId,
        PaymentStatus status,
        CancellationToken cancellationToken = default)
    {
        await _db.PaymentWebhooks
            .Where(x => x.Id == webhookId && x.Status == status)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.IsProcessed, true)
                .SetProperty(x => x.ProcessedAt, DateTime.UtcNow),
                cancellationToken);
    }
}
