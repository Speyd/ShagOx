using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core;
public class PaymentCommandRepository
    : CommandRepository<Payment>,
    IPaymentCommandRepository
{
    public PaymentCommandRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<bool> TryMarkAsFailedAsync(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Payments
            .Where(x =>
                x.InvoiceId == invoiceId &&
                (x.Status == PaymentStatus.Pending ||
                 x.Status == PaymentStatus.Processing))
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.Status,
                        PaymentStatus.Failed),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> TryMarkAsProcessingAsync(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Payments
            .Where(x =>
                x.InvoiceId == invoiceId &&
                x.Status == PaymentStatus.Pending)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.Status,
                        PaymentStatus.Processing),
                cancellationToken);

        return affectedRows > 0;
    }

    public async Task<Payment?> GetSucceededByOrderIdAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Payments
            .AsNoTracking()
            .Where(x =>
                x.OrderId == orderId &&
                x.Status == PaymentStatus.Succeeded)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Payment?> GetActiveByOrderIdAsync(
        long orderId,
        DateTime now,
        CancellationToken cancellationToken = default)
    {
        return await _db.Payments
            .AsNoTracking()
            .Where(x =>
                x.OrderId == orderId &&
                x.ExpiresAt > now &&
                x.PaymentUrl != null &&
                (x.Status == PaymentStatus.Pending ||
                 x.Status == PaymentStatus.Processing))
            .OrderByDescending(x => x.ExpiresAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Payment>> GetOpenByOrderIdAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Payments
            .Include(x => x.Order)
                .ThenInclude(x => x.OrderItems
                    .Where(i => i.EntityStatus == EntityStatus.Active))
            .Where(x =>
                x.OrderId == orderId &&
                (x.Status == PaymentStatus.Pending ||
                 x.Status == PaymentStatus.Processing))
            .ToListAsync(cancellationToken);
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
            .Where(x => x.InvoiceId == invoiceId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.Status,
                        newStatus),
                cancellationToken);


        return affectedRows > 0;
    }
}
