using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public static class OrderCreater
{
    public static Order Create(
        long ownerId,
        string orderNumber,
        OrderCreateRequest request)
    {
        var now = DateTime.UtcNow;
        var city = request.DeliveryCityDto;
        var point = request.DeliveryPointDto;


        return new Order
        {
            OrderNumber = orderNumber,

            OwnerId = ownerId,
            EntityStatus = EntityStatus.Active,

            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            Email = request.Email,

            Comment = request.Comment,

            CurrencyId = request.CurrencyId,

            DeliveryType = request.DeliveryType,
            DeliveryProviderId = request.DeliveryProviderId,

            DeliveryCityExternalId = city.ExternalId,

            DeliveryPointExternalId = point.ExternalId,
            DeliveryPointNumber = point.Number,
            DeliveryPointName = point.Name,
            DeliveryPointAddress = point.Address,
            DeliveryPointType = request.DeliveryPointType,

            PaymentMethod = request.PaymentMethod,

            PaymentStatus = PaymentStatus.Pending,
            OrderStatus = OrderStatus.Pending,

            Subtotal = 0,
            DeliveryPrice = 0,
            TotalPrice = 0,

            CreatedAt = now
        };
    }
}
