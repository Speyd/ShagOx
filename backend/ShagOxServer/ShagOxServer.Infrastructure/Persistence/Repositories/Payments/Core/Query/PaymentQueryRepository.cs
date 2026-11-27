using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core.Query;
public partial class PaymentQueryRepository
    : QueryRepository<Payment>,
      IPaymentQueryRepository
{
    public PaymentQueryRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<Payment?> GetByInvoiceIdAsync(
       string invoiceId,
       CancellationToken cancellationToken = default)
    {
        return await _db.Payments
           .WithIncludes()
           .FirstOrDefaultAsync(c =>
               c.InvoiceId == invoiceId,
               cancellationToken);
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
}
