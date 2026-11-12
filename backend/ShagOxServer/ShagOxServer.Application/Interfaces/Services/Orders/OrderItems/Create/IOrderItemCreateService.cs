using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
public interface IOrderItemCreateService
{
    Task<Result<OrderItem>> CreateAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken = default);
}
