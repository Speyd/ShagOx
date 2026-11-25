using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Delete;
public class ReviewImageDeleteService
    : PictureDeleteService<ReviewImage>,
    IReviewImageDeleteService
{
    public ReviewImageDeleteService(
        IRepository<ReviewImage> imageRepository,
        ICommandRepository<ReviewImage> imageCommand,
        ReviewImageValidator imageValidator,
        PictureValidator pictureValidator,
        IUnitOfWork unitOfWork,
        ILogger<ReviewImageDeleteService> logger
    )
        : base(imageRepository,
            imageCommand,
            imageValidator,
            pictureValidator,
            unitOfWork,
            logger)
    {
    }


    public override async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.DeleteAsync(id, cancellationToken);
        }
        catch
        {

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.ReviewImageDeleteFailed);
        }
    }
    public override async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id,
        CancellationToken cancellationToken = default)
    {

        try
        {
            return await base.DeleteRecordAsync(id, cancellationToken);
        }
        catch
        {
            return Result<DeleteResponse>
                .Fail(EntityErrorResources.ReviewImageDeleteFailed);
        }
    }
}
