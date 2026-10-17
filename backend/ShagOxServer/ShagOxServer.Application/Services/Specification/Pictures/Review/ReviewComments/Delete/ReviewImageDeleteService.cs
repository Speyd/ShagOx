using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Delete;
public class ReviewImageDeleteService
    : PictureDeleteService<ReviewComment, ReviewImage>,
    IReviewImageDeleteService
{
    //private readonly AvatarInvalidationService _avatarInvalid;


    public ReviewImageDeleteService(
        IRepository<ReviewImage> imageRepository,
        ReviewImageValidator imageValidator,
        PictureValidator pictureValidator,
        //ReviewImageInvalidationService avatarInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ReviewImageDeleteService> logger
    )
        : base(imageRepository,
            imageValidator,
            pictureValidator,
            unitOfWork,
            logger)
    {
       // _imageInvalid = imageInvalid;
    }


    public override async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        try
        {
            return await base.DeleteAsync(id);
        }
        catch
        {

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.ReviewImageDeleteFailed);
        }
    }
    public override async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id)
    {

        try
        {
            return await base.DeleteRecordAsync(id);
        }
        catch
        {
            return Result<DeleteResponse>
                .Fail(EntityErrorResources.ReviewImageDeleteFailed);
        }
    }


    public override async Task InvalidateCache(
        ReviewImage image)
    {
        //await _avatarInvalid.InvalidateDeleteAsync(
        //    AvatarCacheMapper.ToInfo(avatar));
    }
}
