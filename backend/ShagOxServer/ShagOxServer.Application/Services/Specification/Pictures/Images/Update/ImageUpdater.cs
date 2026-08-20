using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
public static class ImageUpdater
{
    public static int ApplyUpdates(
        Image image,
        int? newOrder,
        ImageUpdateRequest request)
    {
        var updated = 0;

        if (request.Url is not null &&
            image.Url != request.Url)
        {
            image.Url = request.Url;
            updated++;
        }

        if (newOrder is not null &&
            image.Order != newOrder.Value)
        {
            image.Order = newOrder.Value;
            updated++;
        }

        if (request.AdvertisementId is not null &&
            image.AdvertisementId != request.AdvertisementId.Value)
        {
            image.AdvertisementId = request.AdvertisementId.Value;
            updated++;
        }

        return updated;
    }

    public static int GetNextOrder(
        Advertisement advert)
    {
        return advert.Images
            .Select(x => x.Order)
            .DefaultIfEmpty(0)
            .Max() + 1;
    }
}