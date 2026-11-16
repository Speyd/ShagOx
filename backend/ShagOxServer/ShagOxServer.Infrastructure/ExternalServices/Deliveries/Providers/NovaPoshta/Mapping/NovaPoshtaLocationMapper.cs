using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Query;
using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Application.Services.Deliveries.Providers.NovaPoshta.Mapping;
public static class NovaPoshtaLocationMapper
{
    public static DeliveryCityDto MapCity(
        NovaPoshtaCityDto city)
    {
        return new DeliveryCityDto(
            city.Ref,
            city.Description,
            city.AreaDescription);
    }

    public static DeliveryStreetDto MapStreet(
        NovaPoshtaStreetDto city)
    {
        return new DeliveryStreetDto(
            city.Ref,
            city.Description);
    }

    public static DeliveryPointDto MapPoint(
        NovaPoshtaWarehouseDto point)
    {
        return new DeliveryPointDto
        {
            ExternalId = point.Ref,
            Number = point.Number,
            Name = point.Description,
            Address = point.ShortAddress,
            Type = MapPointType(point.CategoryOfWarehouse)
        };
    }

    private static DeliveryPointType MapPointType(
        string type)
    {
        return type switch
        {
            "Branch" => DeliveryPointType.Branch,
            "Postomat" => DeliveryPointType.ParcelLocker,
            "Store" => DeliveryPointType.ParcelShop,
            "Fulfillment" => DeliveryPointType.Branch,
            _ => throw new ArgumentOutOfRangeException(
                nameof(type),
                type,
                "Unknown Nova Poshta delivery point type")
        };
    }
}
