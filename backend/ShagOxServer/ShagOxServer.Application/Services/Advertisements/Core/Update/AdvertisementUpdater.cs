using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.Core.Update;
public static class AdvertisementUpdater
{
    public static int ApplyUpdates(
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

        if (!string.IsNullOrWhiteSpace(request.Attributes))
        {
            advert.Attributes = JsonDocument.Parse(request.Attributes);
            count++;
        }

        return count;
    }
}