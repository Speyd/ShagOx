using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Payments;

namespace ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
public interface IPaymentQueryRepository
    : IQueryRepository<Payment>
{
    Task<Payment?> GetByInvoiceIdAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
