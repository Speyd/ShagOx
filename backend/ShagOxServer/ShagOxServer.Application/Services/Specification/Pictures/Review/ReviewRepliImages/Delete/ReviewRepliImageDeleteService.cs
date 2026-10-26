using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Delete;
public class ReviewRepliImageDeleteService
    : PictureDeleteService<ReviewRepliImage>,
    IReviewRepliImageDeleteService
{
    private readonly ReviewRepliImageInvalidationService _imageInvalid;


    public ReviewRepliImageDeleteService(
        IRepository<ReviewRepliImage> imageRepository,
        ReviewRepliImageValidator imageValidator,
        PictureValidator pictureValidator,
        ReviewRepliImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ReviewRepliImageDeleteService> logger
    )
        : base(imageRepository,
            imageValidator,
            pictureValidator,
            unitOfWork,
            logger)
    {
        _imageInvalid = imageInvalid;
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
                .Fail(EntityErrorResources.ReviewRepliImageDeleteFailed);
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
                .Fail(EntityErrorResources.ReviewRepliImageDeleteFailed);
        }
    }


    public override async Task InvalidateCache(
        ReviewRepliImage picture)
    {
        await _imageInvalid.InvalidateDeleteAsync(
           PictureCacheMapper.ToInfo(picture));
    }
}
