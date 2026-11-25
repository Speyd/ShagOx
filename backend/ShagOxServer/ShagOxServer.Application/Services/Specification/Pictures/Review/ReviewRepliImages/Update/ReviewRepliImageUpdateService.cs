using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Validator;
using ShagOxServer.Domain.Entities.Comments.Review;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Update;
public class ReviewRepliImageUpdateService
    : PictureOrderUpdateService<ReviewRepliComment,
        ReviewRepliImage,
        PictureOrderUpdateRequest>,
    IReviewRepliImageUpdateService
{
    public ReviewRepliImageUpdateService(
        IRepository<ReviewRepliImage> imageRepository,
        PictureValidator pictureLoadValidator,
        ReviewRepliImageValidator imageValidator,
        IReviewRepliImageDeleteService pictureDeleteService,
        ReviewRepliCommentValidator commentValidator,
        IUnitOfWork unitOfWork,
        ILogger<ReviewRepliImageUpdateService> logger
    )
        : base(imageRepository,
            pictureLoadValidator,
            pictureDeleteService,
            imageValidator,
            commentValidator,
            unitOfWork,
            logger)
    {
    }


    public override async Task<Result<PictureUpdateResponse>> UpdateAsync(
        long imageId,
        PictureOrderUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.UpdateAsync(imageId, 
                request, cancellationToken);
        }
        catch
        {
            return Result<PictureUpdateResponse>.Fail(
                EntityErrorResources.ReviewRepliCommentUpdateFailed);
        }
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
