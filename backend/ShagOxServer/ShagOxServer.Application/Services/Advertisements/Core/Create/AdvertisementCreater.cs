using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create;
public static class AdvertisementCreater
{
    public static Advertisement Create(
       AdvertisementCreateRequest request,
       int userId)
    {
        return new Advertisement
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Stock = request.Stock,
            Popularity = request.Popularity,

            CurrencyId = request.CurrencyId,
            CategoryId = request.CategoryId,
            ConditionId = request.ConditionId,
            SellerId = userId,

            Price = request.Price,
            PreviousPrice = request.Price,

            Properties = request.Properties ?? new()
        };
    }
}