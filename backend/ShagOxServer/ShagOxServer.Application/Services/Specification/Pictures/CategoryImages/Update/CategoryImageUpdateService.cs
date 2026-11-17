using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Update;
public class CategoryImageUpdateService
    : PictureUpdateService<Category,
        CategoryImage,
        PictureUpdateRequest>,
    ICategoryImageUpdateService
{
    private readonly CategoryImageInvalidationService _imageInvalid;


    public CategoryImageUpdateService(
        IRepository<CategoryImage> imageRepository,
        PictureValidator pictureLoadValidator,
        CategoryImageValidator imageValidator,
        ICategoryImageDeleteService imageDeleteService,
        CategoryValidator advertisementValidator,
        CategoryImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CategoryImageUpdateService> logger
    )
        : base(imageRepository,
            pictureLoadValidator,
            imageDeleteService,
            imageValidator,
            advertisementValidator,
            unitOfWork,
            logger)
    {
        _imageInvalid = imageInvalid;
    }


    public override async Task<Result<PictureUpdateResponse>> UpdateAsync(
        long avatarId,
        PictureUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {

            return await base.UpdateAsync(avatarId,
                request, cancellationToken);
        }
        catch
        {
            return Result<PictureUpdateResponse>.Fail(
                EntityErrorResources.CategoryImageUpdateFailed);
        }
    }

    public override async Task InvalidateCache(
        CategoryImage picture)
    {
        await _imageInvalid.InvalidateUpdateAsync(
           PictureCacheMapper.ToInfo(picture));
    }

    protected override int ApplyUpdates(
        CategoryImage picture,
        PictureUpdateRequest request,
        PictureLoaderUploadResponse? response)
    {
        if (response is not null)
        {
            return CategoryImageUpdater.ApplyUpdates(
                picture,
                request,
                response
            );
        }

        return CategoryImageUpdater.ApplyUpdates(
            picture,
            request
        );
    }
}
