using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Update;
public static class AdvertisementUpdater
{
    public static int UpdateFields(
       Advertisement advert,
       AdvertisementUpdateRequest request)
    {
        int count = 0;


        if (request.Title is not null)
        {
            advert.Title = request.Title;
            count++;
        }


        if (request.Description is not null)
        {
            advert.Description = request.Description;
            count++;
        }

        if (request.Stock.HasValue)
        {
            advert.Stock = request.Stock.Value < 0? 0 : request.Stock.Value;
            count++;
        }

        if (request.Price.HasValue)
        {
            var price = request.Price.Value;

            if (price < advert.Price)
                advert.PreviousPrice = advert.Price;

            advert.Price = price;

            count++;
        }


        if (request.Popularity.HasValue)
        {
            advert.Popularity = request.Popularity.Value;
            count++;
        }


        if (request.CurrencyId.HasValue)
        {
            advert.CurrencyId = request.CurrencyId.Value;
            count++;
        }


        if (request.ConditionId.HasValue)
        {
            advert.ConditionId = request.ConditionId.Value;
            count++;
        }


        if (request.CategoryId.HasValue)
        {
            advert.CategoryId = request.CategoryId.Value;
            count++;
        }


        if (request.BuyerId.HasValue)
        {
            advert.BuyerId = request.BuyerId.Value;
            count++;
        }


        if (request.Properties is not null)
        {
            advert.Properties = request.Properties;
            count++;
        }


        return count;
    }
}