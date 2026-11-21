using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Create;
public static class CategoryImageCreater
{
    public static CategoryImage Create(
       PictureCreateRequest request)
    {
        return new CategoryImage
        {
            OwnerId = request.OwnerId,
            Url = "",
            PublicId = ""
        };
    }

    public static CategoryImage Create(
       PictureCreateRequest request,
       PictureLoaderUploadResponse response)
    {
        return new CategoryImage
        {
            OwnerId = request.OwnerId,
            Url = response.Url,
            PublicId = response.PublicId
        };
    }
}
