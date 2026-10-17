using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Create;
public static class AvatarCreater
{
    public static Avatar Create(
       PictureCreateRequest request)
    {
        return new Avatar
        {
            OwnerId = request.OwnerId,
            Url = "",
            PublicId = ""
        };
    }

    public static Avatar Create(
       PictureCreateRequest request,
       PictureLoaderUploadResponse response)
    {
        return new Avatar
        {
            OwnerId = request.OwnerId,
            Url = response.Url,
            PublicId = response.PublicId
        };
    }
}
