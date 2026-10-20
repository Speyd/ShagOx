using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
public static class AdvertisementImageUpdater
{
    public static int ApplyUpdates(
        AdvertisementImage image,
        PictureOrderUpdateRequest request)
    {
        var updated = 0;

        if (request.Order.HasValue)
        {
            image.Order = request.Order.Value;
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

    public static int ApplyUpdates(
        AdvertisementImage image,
        PictureOrderUpdateRequest request,
        PictureLoaderUploadResponse response)
    {
        var updated = 0;

        if (request.Order.HasValue)
        {
            image.Order = request.Order.Value;
            updated++;
        }

        if (request.OwnerId is not null &&
            image.OwnerId != request.OwnerId.Value)
        {
            image.OwnerId = request.OwnerId.Value;
            updated++;
        }

        if (!string.IsNullOrWhiteSpace(response.Url))
        {
            image.Url = response.Url;
            updated++;
        }

        if (!string.IsNullOrWhiteSpace(response.PublicId))
        {
            image.PublicId = response.PublicId;
            updated++;
        }

        return updated;
    }
}
