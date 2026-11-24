using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Providers.Payment;
public interface IPaymentProvider
{
    Task<Result<PaymentResponse>> PaymentRequest(
        PaymentRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> WebHook(
        HttpRequest request,
        CancellationToken cancellationToken = default);

}
