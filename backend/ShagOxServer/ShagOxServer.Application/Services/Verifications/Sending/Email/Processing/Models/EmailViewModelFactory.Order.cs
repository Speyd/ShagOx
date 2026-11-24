using ShagOxServer.Application.DTOs.Orders.Core.Query.Email.Confirmed;
using ShagOxServer.Application.DTOs.Orders.Core.Query.Email.Create;
using ShagOxServer.Application.Resources.Orders.OrderConfirmedEmail;
using ShagOxServer.Application.Resources.Orders.OrderPaidEmail;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
public static partial class EmailViewModelFactory
{
    public static dynamic CreateOrderPaid(
        OrderPaidEmailModel model)
    {
        return new
        {
            OrderNumber = model.OrderNumber,
            CustomerEmail = model.CustomerEmail,
            CustomerName = model.CustomerName,
            CurrencyCode = model.CurrencyCode,
            CurrencySymbol = model.CurrencySymbol,
            Subtotal = model.Subtotal,
            DeliveryPrice = model.DeliveryPrice,
            TotalPrice = model.TotalPrice,
            OrderUrl = model.OrderUrl,
            Items = model.Items,

            OrderSuccess = OrderPaidEmailResources.OrderSuccess,
            ThankYou = OrderPaidEmailResources.ThankYou,
            OrderTitle = OrderPaidEmailResources.Order,

            Greeting = string.Format(
                OrderPaidEmailResources.Greeting,
                model.CustomerName
            ),

            YourProducts = OrderPaidEmailResources.YourProducts,
            Quantity = OrderPaidEmailResources.Quantity,
            Products = OrderPaidEmailResources.Products,
            Delivery = OrderPaidEmailResources.Delivery,
            Total = OrderPaidEmailResources.Total,
            ViewOrder = OrderPaidEmailResources.ViewOrder,
            Footer = OrderPaidEmailResources.Footer
        };
    }

    public static dynamic CreateOrderConfirmed(
        OrderConfirmedEmailModel model)
    {
        return new
        {
            OrderNumber = model.OrderNumber,
            CustomerEmail = model.CustomerEmail,
            OrderUrl = model.OrderUrl,

            OrderConfirmed = OrderConfirmedEmailResources.OrderConfirmed,
            ConfirmationMessage = OrderConfirmedEmailResources.ConfirmationMessage,
            DeliveryMessage = OrderConfirmedEmailResources.DeliveryMessage,
            OrderTitle = OrderConfirmedEmailResources.OrderTitle,
            ViewOrder = OrderConfirmedEmailResources.ViewOrder,
            Footer = OrderConfirmedEmailResources.Footer
        };
    }
}
