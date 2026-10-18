using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Create;
public static class ReviewImageCreater
{
    public static ReviewImage Create(
       PictureOrderCreateRequest request)
    {
        return new ReviewImage
        {
            OwnerId = request.OwnerId,
            Order = request.Order,
            PublicId = "",
            Url = ""
        };
    }

    public static ReviewImage Create(
        PictureOrderCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return new ReviewImage
        {
            OwnerId = request.OwnerId,
            Order = request.Order,
            PublicId = response.PublicId,
            Url = response.Url,
        };
    }
}
