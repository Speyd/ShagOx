using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Payments;
public interface IPaymentProcessingService
{
    Task<Result<bool>> ProcessSuccessfulPayment(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
