using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Text.Json;

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
            StatusId = request.StatusId,
            ConditionId = request.ConditionId,
            OwnerId = userId,

            Attributes = string.IsNullOrWhiteSpace(request.Attributes)
                ? JsonDocument.Parse("{}")
                : JsonDocument.Parse(request.Attributes)
        };
    }
}
