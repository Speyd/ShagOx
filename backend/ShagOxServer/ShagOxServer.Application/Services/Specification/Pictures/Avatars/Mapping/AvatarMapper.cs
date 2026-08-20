using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
public static class AvatarMapper
{
    public static AvatarDto ToDto(
        Avatar avatar)
    {
        return new AvatarDto(
            avatar.Id,
            avatar.Url,
            avatar.PublicId,
            avatar.UserId
        );
    }
}