using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Payments;
public interface IPaymentProcessingService
{
    Task<Result<bool>> ProcessFailedPayment(
        string invoiceId,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> ProcessFailedPayment(
        ShagOxServer.Domain.Entities.Payments.Payment payment,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> ProcessSuccessfulPayment(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
