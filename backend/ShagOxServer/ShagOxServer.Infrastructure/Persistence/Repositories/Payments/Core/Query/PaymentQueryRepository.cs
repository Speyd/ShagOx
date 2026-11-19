using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Domain.Entities.Payments;
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
        ReplicaDbContext db)
        : base(db)
    { }


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
}
