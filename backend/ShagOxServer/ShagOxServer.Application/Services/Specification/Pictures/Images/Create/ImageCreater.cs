using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
public static class ImageCreater
{
    public static Image Create(
       ImageCreateRequest request)
    {
        return new Image
        {
            Url = request.Url,
            Order = request.Order,
            AdvertisementId = request.AdvertisementId,
            PublicId = ""
        };
    }

    public static Image Create(
        ImageFileCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return new Image
        {
            Url = response.Url,
            Order = request.Order,
            AdvertisementId = request.AdvertisementId,
            PublicId = response.PublicId
        };
    }
}