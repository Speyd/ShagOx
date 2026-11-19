using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public static class OrderCreater
{
    public static Order Create(
        long ownerId,
        string orderNumber,
        OrderCreateRequest request)
    {
        var now = DateTime.UtcNow;
        var point = request.DeliveryPointDto;
        var city = request.RecipientCityDto;
        var street = request.RecipientStreetDto;


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

            RecipientCityExternalId = city?.ExternalId,

            DeliveryPointExternalId = point?.ExternalId,
            DeliveryPointNumber = point?.Number,
            DeliveryPointName = point?.Name,
            DeliveryPointAddress = point?.Address,
            DeliveryPointType = request?.DeliveryPointType,

            RecipientStreetExternalId = street?.ExternalId,
            RecipientBuildingNumber = request?.RecipientBuildingNumber,
            RecipientFlatNumber = request?.RecipientFlatNumber,

            PaymentMethod = request!.PaymentMethod,

            PaymentStatus = PaymentStatus.Pending,
            OrderStatus = OrderStatus.Pending,

            Subtotal = 0,
            DeliveryPrice = 0,
            TotalPrice = 0,

            CreatedAt = now
        };
    }
}
