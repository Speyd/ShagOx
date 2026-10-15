using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
public static class AdvertisementImageUpdater
{
    public static int ApplyUpdates(
        AdvertisementImage image,
        int? newOrder,
        PictureOrderUpdateRequest request)
    {
        var updated = 0;

        if (newOrder is not null &&
            image.Order != newOrder.Value)
        {
            image.Order = newOrder.Value;
            updated++;
        }

        if (request.OwnerId is not null &&
            image.OwnerId != request.OwnerId.Value)
        {
            image.OwnerId = request.OwnerId.Value;
            updated++;
        }

        return updated;
    }
}
