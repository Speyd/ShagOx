using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
public interface IPaymentCommandsRepository
{
    Task<bool> TryMarkAsPaidAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    Task<bool> TryUpdatePaymentStatusAsync(
        string invoiceId,
        PaymentStatus newStatus,
        CancellationToken cancellationToken = default);

    Task<bool> TryMarkAsFailedAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    Task<bool> TryMarkAsProcessingAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
