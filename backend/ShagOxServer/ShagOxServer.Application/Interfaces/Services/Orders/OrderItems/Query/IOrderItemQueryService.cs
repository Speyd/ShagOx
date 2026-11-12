using ShagOxServer.Application.DTOs.LineItems.Query;
using ShagOxServer.Application.DTOs.Orders.OrderItems.Query;
using ShagOxServer.Application.Interfaces.Services.LineItems.Core.Query;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.OrderItems;

namespace ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Query;
public interface IOrderItemQueryService
    : ILineItemQueryService<OrderItemDto,
        OrderItem,
        OrderItemSearchFilter>
{
}
