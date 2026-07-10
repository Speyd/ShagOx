using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Update;
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


        if (request.Price is not null)
        {
            var price = request.Price.Value;

            if (price < advert.Price)
                advert.PreviousPrice = advert.Price;

            advert.Price = price;

            count++;
        }


        if (request.Popularity is not null)
        {
            advert.Popularity = request.Popularity.Value;
            count++;
        }


        if (request.CurrencyId is not null)
        {
            advert.CurrencyId = request.CurrencyId.Value;
            count++;
        }


        if (request.ConditionId is not null)
        {
            advert.ConditionId = request.ConditionId.Value;
            count++;
        }


        if (request.CategoryId is not null)
        {
            advert.CategoryId = request.CategoryId.Value;
            count++;
        }


        if (request.BuyerId is not null)
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