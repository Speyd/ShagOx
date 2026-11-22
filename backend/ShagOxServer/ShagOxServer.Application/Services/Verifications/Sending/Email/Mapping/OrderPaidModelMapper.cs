using ShagOxServer.Application.DTOs.Orders.Core.Query.Email;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Mapping;
public static class OrderPaidModelMapper
{
    public static OrderPaidEmailModel ToDto(
        Order order,
        string orderUrl)
    {
        return new OrderPaidEmailModel(
            order.OrderNumber,
            order.FirstName + order.LastName,
            order.Email,
            order.Currency.Code,
            order.Currency.Symbol,
            order.Subtotal,
            order.DeliveryPrice,
            order.TotalPrice,
            orderUrl,
            order.OrderItems
                .Select(OrderEmailItemMapper.ToDto)
                .ToList()
            );
    }
}
