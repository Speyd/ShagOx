using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Create;
public partial class ReviewRepliImageCreateService
    : PictureOrderCreateService<ReviewRepliComment,
        ReviewRepliImage,
        PictureOrderCreateRequest>,
    IReviewRepliImageCreateService
{
    private protected ReviewRepliImageInvalidationService _imageInvalidation;

    public ReviewRepliImageCreateService(
        IRepository<ReviewRepliImage> imageRepository,
        PictureValidator pictureValidator,
        ReviewRepliCommentValidator commentValidator,
        ReviewRepliImageInvalidationService imageInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<ReviewRepliImageCreateService> logger
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
                EntityErrorResources.ReviewRepliImageCreateFailed);
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
                EntityErrorResources.ReviewRepliImageCreateFailed);
        }
    }

    protected override ReviewRepliImage CreatePicture(
        PictureOrderCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return ReviewRepliImageCreater
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
        ReviewRepliImage picture)
    {
        await _imageInvalidation.InvalidateCreateAsync(
            PictureCacheMapper.ToInfo(picture));
    }
}
