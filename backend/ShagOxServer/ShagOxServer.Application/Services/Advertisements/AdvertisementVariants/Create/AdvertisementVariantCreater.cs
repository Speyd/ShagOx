using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Create;
public static class AdvertisementVariantCreater
{
    public static AdvertisementVariant Create(
       AdvertisementVariantCreateRequest request)
    {
        return new AdvertisementVariant
        {
            AdvertisementId = request.AdvertisementId,
            Price = request.Price,
            PreviousPrice = request.PreviousPrice,
            Stock = request.Stock,
            Attributes = request.Attributes ?? JsonDocument.Parse("{}")
        };
    }
}