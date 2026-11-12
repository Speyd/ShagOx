using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Delete;
public interface IOrderItemDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<OrderItem, DeleteResponse>
{
}
