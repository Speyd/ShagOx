using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Update;
public static class AdvertisementVariantUpdater
{
    public static int ApplyUpdates(
       AdvertisementVariant variant,
       AdvertisementVariantUpdateRequest request)
    {
        int count = 0;


        if (request.AdvertisementId.HasValue)
        {
            variant.AdvertisementId = request.AdvertisementId.Value;
            count++;
        }

        if (request.Price.HasValue)
        {
            var price = request.Price.Value;

            if (price < variant.Price)
                variant.PreviousPrice = variant.Price;

            variant.Price = price;

            count++;
        }

        if (request.Stock.HasValue)
        {
            variant.Stock = request.Stock.Value;
            count++;
        }

        if (request.Attributes is not null)
        {
            variant.Attributes = request.Attributes;
            count++;
        }


        return count;
    }
}