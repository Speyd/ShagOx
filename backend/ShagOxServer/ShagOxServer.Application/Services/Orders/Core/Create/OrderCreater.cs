using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public static class OrderCreater
{
    public static Order Create(
        long ownerId,
        OrderCreateRequest request)
    {
        var now = DateTime.UtcNow;

        return new Order
        {
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

            DeliveryPointExternalId = request.DeliveryPointExternalId,
            DeliveryPointNumber = request.DeliveryPointNumber,
            DeliveryPointName = request.DeliveryPointName,
            DeliveryPointAddress = request.DeliveryPointAddress,
            DeliveryPointType = request.DeliveryPointType,

            DeliveryAddress = request.DeliveryAddress,

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
