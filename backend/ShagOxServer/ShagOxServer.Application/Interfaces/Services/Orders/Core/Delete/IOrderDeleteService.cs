using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Interfaces.Services.Orders.Core.Delete;
public interface IOrderDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Order, DeleteResponse>
{
}
