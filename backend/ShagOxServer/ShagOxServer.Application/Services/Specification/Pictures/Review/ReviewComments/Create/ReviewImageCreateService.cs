using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Create;
public partial class ReviewImageCreateService
    : PictureOrderCreateService<ReviewComment,
        ReviewImage,
        PictureOrderCreateRequest>,
    IReviewImageCreateService
{
    private protected ReviewImageInvalidationService _imageInvalidation;

    public ReviewImageCreateService(
        IRepository<ReviewImage> imageRepository,
        PictureValidator pictureValidator,
        ReviewCommentValidator commentValidator,
        ReviewImageInvalidationService imageInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<ReviewImageCreateService> logger
    )
        : base(imageRepository,
            pictureValidator,
            commentValidator,
            unitOfWork,
            logger)
    {
        _imageInvalidation = imageInvalidation;
    }


    public override async Task<Result<PictureCreateResponse>> CreateAsync(
        PictureOrderCreateRequest request)
    {
        try
        {
            return await base.CreateAsync(request);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.ReviewImageCreateFailed);
        }
    }

    public override async Task<Result<PictureCreateResponse>> CreateInternalAsync(
       PictureOrderCreateRequest request)
    {
        try
        {
            return await base.CreateInternalAsync(request);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.ReviewImageCreateFailed);
        }
    }

    public override async Task<Result<PicturesCreateResponse>> CreateAsync(
        PicturesCreateRequest request)
    {
        try
        {
            return await base.CreateAsync(request);
        }
        catch
        {
            return Result<PicturesCreateResponse>.Fail(
                EntityErrorResources.ReviewImageCreateFailed);
        }
    }

    protected override ReviewImage CreatePicture(
        PictureOrderCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return ReviewImageCreater
            .Create(request, response);
    }

    protected override PictureOrderCreateRequest CreatePictureOrderRequest(
        IFormFile file,
        long ownerId,
        int order)
    {
        return new PictureOrderCreateRequest(
            file,
            ownerId,
            order
        );
    }

    public override async Task InvalidateCache(
        ReviewImage picture)
    {
        await _imageInvalidation.InvalidateCreateAsync(
            PictureCacheMapper.ToInfo(picture));
    }
}
