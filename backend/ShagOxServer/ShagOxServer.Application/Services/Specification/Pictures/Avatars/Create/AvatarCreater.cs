using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Create;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Create;
public static class AvatarCreater
{
    public static Avatar Create(
       AvatarCreateRequest request)
    {
        return new Avatar
        {
            UserId = request.UserId,
            Url = "",
            PublicId = ""
        };
    }

    public static Avatar Create(
       AvatarCreateRequest request,
       PictureLoaderUploadResponse response)
    {
        return new Avatar
        {
            Url = response.Url,
            UserId = request.UserId,
            PublicId = response.PublicId
        };
    }
}