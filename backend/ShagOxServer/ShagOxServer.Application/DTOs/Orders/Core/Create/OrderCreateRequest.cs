using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

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
    DeliveryPointDto? DeliveryPointDto,
    DeliveryCityDto? RecipientCityDto,
    DeliveryStreetDto? RecipientStreetDto,
    string? RecipientBuildingNumber,
    string? RecipientFlatNumber,
    DeliveryPointType? DeliveryPointType,
    PaymentMethod PaymentMethod
);
