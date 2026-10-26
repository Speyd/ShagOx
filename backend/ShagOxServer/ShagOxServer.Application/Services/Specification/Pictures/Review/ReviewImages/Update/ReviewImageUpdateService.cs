using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Update;
public class ReviewImageUpdateService
    : PictureOrderUpdateService<ReviewComment,
        ReviewImage,
        PictureOrderUpdateRequest>,
    IReviewImageUpdateService
{
    private readonly ReviewImageInvalidationService _imageInvalid;


    public ReviewImageUpdateService(
        IRepository<ReviewImage> imageRepository,
        PictureValidator pictureLoadValidator,
        ReviewImageValidator imageValidator,
        ReviewCommentValidator commentValidator,
        ReviewImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ReviewImageUpdateService> logger
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
                EntityErrorResources.ReviewCommentUpdateFailed);
        }
    }

    public override async Task InvalidateCache(
        ReviewImage picture)
    {
        await _imageInvalid.InvalidateUpdateAsync(
           PictureCacheMapper.ToInfo(picture));
    }

    protected override int ApplyUpdates(
        ReviewImage picture,
        PictureOrderUpdateRequest request,
        PictureLoaderUploadResponse? response)
    {
        if (response is not null)
        {
            return ReviewImageUpdater.ApplyUpdates(
                picture,
                request,
                response
            );
        }

        return ReviewImageUpdater.ApplyUpdates(
            picture,
            request
        );
    }
}
