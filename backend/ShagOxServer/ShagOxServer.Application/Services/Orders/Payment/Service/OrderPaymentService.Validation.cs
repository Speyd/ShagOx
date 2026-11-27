using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Services.Orders.Payment;
public partial class OrderPaymentService
{
    private static string? ValidatePayable(Order order, DateTime now)
    {
        if (order.PaymentMethod != PaymentMethod.Online)
            return PaymentResources.OrderPaymentNotRequired;

        if (order.PaymentStatus == OrderPaymentStatus.Paid)
            return PaymentResources.OrderAlreadyPaid;

        if (!OrderPaymentPolicy.CanPay(order, now))
            return PaymentResources.OrderPaymentUnavailable;

        return null;
    }
}
