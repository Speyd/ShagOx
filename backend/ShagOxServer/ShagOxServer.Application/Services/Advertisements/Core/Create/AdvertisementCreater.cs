using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create;
public static class AdvertisementCreater
{
    public static Advertisement Create(
       AdvertisementCreateRequest request,
       long userId)
    {
        return new Advertisement
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Popularity = request.Popularity,

            CurrencyId = request.CurrencyId,
            CategoryId = request.CategoryId,
            ConditionId = request.ConditionId,
            SellerId = userId,

            Attributes = request.Attributes ?? new()
        };
    }
}