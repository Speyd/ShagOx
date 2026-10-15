using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
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
}
