using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core;
public class OrderCommandRepository
    : IOrderCommandRepository
{
    private readonly AppDbContext _db;

    public OrderCommandRepository(
        AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> TryMarkAsPaidAsync(
        string invoiceId,
        PaymentStatus paymentStatus,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Orders
            .Where(x =>
                x.InvoiceId == invoiceId &&
                x.PaymentStatus != PaymentStatus.Paid)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.PaymentStatus,
                        PaymentStatus.Paid),
                cancellationToken);

        return affectedRows > 0;
    }
}
