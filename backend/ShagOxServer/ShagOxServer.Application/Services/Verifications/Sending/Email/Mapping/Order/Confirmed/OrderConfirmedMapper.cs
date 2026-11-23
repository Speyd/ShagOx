using ShagOxServer.Application.DTOs.Orders.Core.Query.Email.Confirmed;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Mapping.Order.Confirmed;
public static class OrderConfirmedMapper
{
    public static OrderConfirmedEmailModel ToDto(
        ShagOxServer.Domain.Entities.Orders.Order order,
        string orderUrl)
    {
        return new OrderConfirmedEmailModel(
           order.OrderNumber,
           orderUrl,
           order.Email);
    }
}
