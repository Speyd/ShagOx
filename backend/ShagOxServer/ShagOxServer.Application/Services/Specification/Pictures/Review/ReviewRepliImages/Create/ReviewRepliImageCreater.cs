using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Create;
public static class ReviewRepliImageCreater
{
    public static ReviewRepliImage Create(
       PictureOrderCreateRequest request)
    {
        return new ReviewRepliImage
        {
            OwnerId = request.OwnerId,
            Order = request.Order,
            PublicId = "",
            Url = ""
        };
    }

    public static ReviewRepliImage Create(
        PictureOrderCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return new ReviewRepliImage
        {
            OwnerId = request.OwnerId,
            Order = request.Order,
            PublicId = response.PublicId,
            Url = response.Url,
        };
    }
}
