using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders.Enum;

namespace ShagOxServer.Application.DTOs.Orders.Core.Create;
public sealed record OrderCreateRequest
(
    string FirstName,
    string LastName,
    string Phone,
    string Email,
    ICollection<LineItemCreateRequest> OrderItems,
    string? Comment,
    long CurrencyId,
    DeliveryType DeliveryType,
    long? DeliveryProviderId,
    DeliveryCityDto DeliveryCityDto,
    DeliveryPointDto DeliveryPointDto,
    DeliveryPointType? DeliveryPointType,
    PaymentMethod PaymentMethod
);
