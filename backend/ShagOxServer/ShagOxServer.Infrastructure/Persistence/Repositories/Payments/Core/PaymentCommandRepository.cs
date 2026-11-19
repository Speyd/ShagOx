using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core;
public class PaymentCommandRepository
    : IPaymentCommandRepository
{
    private readonly AppDbContext _db;

    public PaymentCommandRepository(
        AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> TryMarkAsPaidAsync(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Payments
            .Where(x =>
                x.InvoiceId == invoiceId &&
                x.Status != PaymentStatus.Succeeded)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.Status,
                        PaymentStatus.Succeeded),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryUpdatePaymentStatusAsync(
        string invoiceId,
        PaymentStatus newStatus,
        CancellationToken cancellationToken = default)
    {
        if (newStatus is not (PaymentStatus.Failed or PaymentStatus.Refunded))
            throw new ArgumentException(
                "Only Failed or Refunded statuses are allowed.",
                nameof(newStatus));

        var affectedRows = await _db.Payments
            .Where(x =>
                x.InvoiceId == invoiceId &&
                x.Status != PaymentStatus.Succeeded)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.Status,
                        newStatus),
                cancellationToken);

        return affectedRows > 0;
    }
}
