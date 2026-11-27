using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Services.Orders.Payment;
public static class OrderPaymentPolicy
{
    public static readonly TimeSpan RetryWindow = TimeSpan.FromDays(3);
    public static readonly TimeSpan ProcessingGracePeriod = TimeSpan.FromMinutes(5);


    public static bool IsAwaitingPayment(Order order)
    {
        if (order.PaymentMethod != PaymentMethod.Online)
            return false;

        return
            (order.OrderStatus == OrderStatus.Pending &&
             order.PaymentStatus == OrderPaymentStatus.Pending)
            ||
            (order.OrderStatus == OrderStatus.Cancelled &&
             order.PaymentStatus == OrderPaymentStatus.Failed);
    }

    public static DateTime GetPaymentDeadline(Order order)
    {
        return order.CreatedAt + RetryWindow;
    }

    public static bool CanPay(Order order, DateTime now)
    {
        return IsAwaitingPayment(order) &&
            now < GetPaymentDeadline(order);
    }

    public static bool IsExpired(
        ShagOxServer.Domain.Entities.Payments.Payment payment,
        DateTime now)
    {
        return payment.Status switch
        {
            PaymentStatus.Pending => payment.ExpiresAt <= now,
            PaymentStatus.Processing => payment.ExpiresAt + ProcessingGracePeriod <= now,
            _ => false
        };
    }
}
