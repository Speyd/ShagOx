using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Images.Create;
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
        ImageLoaderUploadResponse response)
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