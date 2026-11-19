using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.PaymentWebhooks;
public class PaymentWebhookCommandRepository 
    : IPaymentWebhookCommandRepository
{
    private readonly AppDbContext _db;


    public PaymentWebhookCommandRepository(
        AppDbContext db)
    {
        _db = db;
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

}
