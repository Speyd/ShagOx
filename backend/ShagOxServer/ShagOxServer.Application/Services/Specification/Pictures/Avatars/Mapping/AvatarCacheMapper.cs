using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Cache;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
public static class AvatarCacheMapper
{
    public static AvatarCacheInfo ToInfo(
        Avatar avatar)
    {
        return new AvatarCacheInfo(
            avatar.Id,
            avatar.UserId
        );
    }
}