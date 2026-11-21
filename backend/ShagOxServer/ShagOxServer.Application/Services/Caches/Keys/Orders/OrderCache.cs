using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Keys.Orders;
public static class OrderCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<Order>();

    public static string ByOrderNumber(
       string orderNumber,
       string language,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status:{status}:orderNumber:{orderNumber}";

    public static string ByOrderNumberPattern(
       string orderNumber,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:orderNumber:{orderNumber}";

    public static string ByOrderNumberPattern(
       string orderNumber)
           => $"{Prefix}:*:orderNumber:{orderNumber}";




    public static string ByPaymentTransaction(
       string paymentTransactionId,
       string language,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status:{status}:paymentTransactionId:{paymentTransactionId}";

    public static string ByPaymentTransactionPattern(
       string paymentTransactionId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:paymentTransactionId:{paymentTransactionId}";

    public static string ByPaymentTransactionPattern(
       string paymentTransactionId)
           => $"{Prefix}:*:paymentTransactionId:{paymentTransactionId}";



  
    public static string ByTrackingNumber(
       string trackingNumber,
       string language,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status:{status}:trackingNumber:{trackingNumber}";

    public static string ByTrackingNumberPattern(
       string trackingNumber,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:trackingNumber:{trackingNumber}";

    public static string ByTrackingNumberPattern(
       string trackingNumber)
           => $"{Prefix}:*:trackingNumber:{trackingNumber}";



    public static string ByUser(
       long userId,
       string language,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:{language}:status:{status}:user:{userId}";

    public static string ByUserPattern(
        long userId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:user:{userId}";

    public static string ByUserPattern(
        long userId)
           => $"{Prefix}:*:user:{userId}";
}
