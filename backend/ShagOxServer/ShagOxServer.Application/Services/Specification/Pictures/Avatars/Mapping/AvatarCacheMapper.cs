using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
public static class AvatarCacheMapper
{
    public static PictureCacheInfo ToInfo(
        Avatar avatar)
    {
        return new PictureCacheInfo(
            avatar.Id,
            avatar.OwnerId
        );
    }
}
