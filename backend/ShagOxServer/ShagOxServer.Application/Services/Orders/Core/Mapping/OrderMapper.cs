using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.DTOs.Orders.Core.Query;
using ShagOxServer.Application.DTOs.Orders.OrderItems.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Orders.Core.Mapping;
public static class OrderMapper
{
    public static OrderDto ToDto(
        Order order,
        List<OrderItemDto> items,
        CurrencyDto currency,
        DeliveryProviderDto? delivery,
        ConvertedOrderPricesDto convertedPrices)
    {
        return new OrderDto(
            order.Id,
            order.EntityStatus,
            order.OrderNumber,
            order.FirstName,
            order.LastName,
            order.Phone,
            order.Email,
            items,
            order.Comment,
            order.Subtotal,
            convertedPrices.SubtotalConversion,
            order.DeliveryPrice,
            convertedPrices.DeliveryPriceConversion,
            order.TotalPrice,
            convertedPrices.TotalPriceConversion,
            currency,
            order.DeliveryType,
            delivery,
            order.DeliveryPointExternalId,
            order.DeliveryPointNumber,
            order.DeliveryPointName,
            order.DeliveryPointAddress,
            order.DeliveryPointType,
            order.DeliveryTrackingNumber,
            order.DeliveryTrackingUrl,
            order.ShippedAt,
            order.DeliveredAt,
            order.EstimatedDeliveryAt,
            order.PaymentMethod,
            order.PaymentStatus,
            order.PaidAt,
            order.PaymentTransactionId,
            order.PaymentProvider,
            order.OrderStatus,
            order.ConfirmedAt,
            order.CreatedAt,
            order.UpdatedAt,
            order.CancelledAt
        );
    }
}
