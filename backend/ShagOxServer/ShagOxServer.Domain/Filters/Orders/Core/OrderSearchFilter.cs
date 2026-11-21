using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Domain.Filters.Orders.Core;
public sealed record OrderSearchFilter
(
    long? OwnerId,
    string? SearchTerm,
    string? OrderNumber,

    string? FirstName,
    string? LastName,
    string? Phone,
    string? Email,

    decimal? MinTotalPrice,
    decimal? MaxTotalPrice,
    long? CurrencyId,

    DeliveryType? DeliveryType,
    long? DeliveryProviderId,
    string? DeliveryTrackingNumber,
    DeliveryPointType? DeliveryPointType,

    PaymentMethod? PaymentMethod,
    OrderPaymentStatus? PaymentStatus,

    OrderStatus? OrderStatus,

    DateTime? CreatedFrom,
    DateTime? CreatedTo,
    DateTime? PaidFrom,
    DateTime? PaidTo,
    DateTime? ShippedFrom,
    DateTime? ShippedTo,
    DateTime? DeliveredFrom,
    DateTime? DeliveredTo,

    // Замовлення, усі товари яких належать цьому продавцю.
    long? SellerId = null,
    // Приховати онлайн-замовлення, які ще не оплачені (продавець бачить
    // онлайн-замовлення лише після оплати).
    bool? HideUnpaidOnline = null
) : BaseFilter();
