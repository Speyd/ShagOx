using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Update;
public static class AvatarUpdater
{
    public static int ApplyUpdates(
       Avatar avatar,
       PictureUpdateRequest request)
    {
        var updated = 0;

        if (request.OwnerId is not null)
        {
            avatar.OwnerId = request.OwnerId.Value;
            updated++;
        }

        return updated;
    }

    public static int ApplyUpdates(
        Avatar avatar,
        PictureUpdateRequest request,
        PictureLoaderUploadResponse response)
    {
        var updated = 0;

        if (request.OwnerId is not null)
        {
            avatar.OwnerId = request.OwnerId.Value;
            updated++;
        }

        if (!string.IsNullOrWhiteSpace(response.Url))
        {
            avatar.Url = response.Url;
            updated++;
        }

        if (!string.IsNullOrWhiteSpace(response.PublicId))
        {
            avatar.PublicId = response.PublicId;
            updated++;
        }

        return updated;
    }
}
