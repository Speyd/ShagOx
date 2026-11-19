using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
public interface IOrderCommandRepository
{
    Task<bool> TryMarkAsPaidAsync(
        string invoiceId,
        PaymentStatus paymentStatus,
        CancellationToken cancellationToken = default);
}
