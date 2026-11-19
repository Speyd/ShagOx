using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
public interface IPaymentCommandRepository
{
    Task<bool> TryMarkAsPaidAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    Task<bool> TryUpdatePaymentStatusAsync(
        string invoiceId,
        PaymentStatus newStatus,
        CancellationToken cancellationToken = default);
}
