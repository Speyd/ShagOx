using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
public static class AdvertisementImageCreater
{
    public static AdvertisementImage Create(
       PictureOrderCreateRequest request)
    {
        return new AdvertisementImage
        {
            OwnerId = request.OwnerId,
            Order = request.Order,
            PublicId = "",
            Url = ""
        };
    }

    public static AdvertisementImage Create(
        PictureOrderCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return new AdvertisementImage
        {
            OwnerId = request.OwnerId,
            Order = request.Order,
            PublicId = response.PublicId,
            Url = response.Url,
        };
    }
}
