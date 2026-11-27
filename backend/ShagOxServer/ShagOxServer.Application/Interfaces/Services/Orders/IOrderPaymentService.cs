using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Orders;
public interface IOrderPaymentService
{
    Task<Result<OrderPaymentResponse>> GetOrCreatePaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default);
}
