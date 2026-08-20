using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Update;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Update;
public static class AvatarUpdater
{
    public static int ApplyUpdates(
       Avatar avatar,
       AvatarUpdateRequest request)
    {
        var updated = 0;

        if (request.Url is not null )
        {
            avatar.Url = request.Url;
            updated++;
        }

        if (request.UserId is not null)
        {
            avatar.UserId = request.UserId.Value;
            updated++;
        }

        return updated;
    }
}