using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Payments;
public interface IPaymentWebhookProcessor
{
    Task<Result<bool>> ReceiveAsync(
        WebhookDto webhook,
        string payload,
        CancellationToken cancellationToken = default);

    Task ProcessPendingAsync(
        int count,
        CancellationToken cancellationToken = default);

    //Task<Result<bool>> ProcessPendingAsync(
    //    string invoiceId,
    //    CancellationToken cancellationToken = default);
}
