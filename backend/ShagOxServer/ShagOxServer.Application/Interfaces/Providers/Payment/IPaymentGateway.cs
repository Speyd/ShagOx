using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Providers.Payment;
public interface IPaymentGateway
{
    Task<Result<bool>> RefundAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> CancelInvoiceAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
