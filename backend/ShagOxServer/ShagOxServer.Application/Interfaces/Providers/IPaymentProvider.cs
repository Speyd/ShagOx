using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Providers;
public interface IPaymentProvider
{
    Task<Result<PaymentResponse>> PaymentRequest(
        PaymentRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> WebHook(
        HttpRequest request,
        CancellationToken cancellationToken = default);

}
