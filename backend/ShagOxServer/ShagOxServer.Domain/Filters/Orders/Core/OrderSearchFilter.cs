using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Domain.Filters.Orders.Core;
public sealed record OrderSearchFilter
(
    long? SellerId,
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
    bool? HideUnpaidOnline = null
) : BaseFilter();
