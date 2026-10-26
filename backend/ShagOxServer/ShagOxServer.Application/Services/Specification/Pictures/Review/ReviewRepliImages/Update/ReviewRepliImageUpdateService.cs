using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Update;
public class ReviewRepliImageUpdateService
    : PictureOrderUpdateService<ReviewRepliComment,
        ReviewRepliImage,
        PictureOrderUpdateRequest>,
    IReviewRepliImageUpdateService
{
    private readonly ReviewRepliImageInvalidationService _imageInvalid;


    public ReviewRepliImageUpdateService(
        IRepository<ReviewRepliImage> imageRepository,
        PictureValidator pictureLoadValidator,
        ReviewRepliImageValidator imageValidator,
        ReviewRepliCommentValidator commentValidator,
        ReviewRepliImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ReviewRepliImageUpdateService> logger
    )
        : base(imageRepository,
            pictureLoadValidator,
            imageValidator,
            commentValidator,
            unitOfWork,
            logger)
    {
        _imageInvalid = imageInvalid;
    }


    public override async Task<Result<PictureUpdateResponse>> UpdateAsync(
        long imageId,
        PictureOrderUpdateRequest request)
    {
        try
        {
            return await base.UpdateAsync(imageId, request);
        }
        catch
        {
            return Result<PictureUpdateResponse>.Fail(
                EntityErrorResources.ReviewRepliCommentUpdateFailed);
        }
    }

    public override async Task InvalidateCache(
        ReviewRepliImage picture)
    {
        await _imageInvalid.InvalidateUpdateAsync(
           PictureCacheMapper.ToInfo(picture));
    }

    protected override int ApplyUpdates(
        ReviewRepliImage picture,
        PictureOrderUpdateRequest request,
        PictureLoaderUploadResponse? response)
    {
        if (response is not null)
        {
            return ReviewRepliImageUpdater.ApplyUpdates(
                picture,
                request,
                response
            );
        }

        return ReviewRepliImageUpdater.ApplyUpdates(
            picture,
            request
        );
    }
}
