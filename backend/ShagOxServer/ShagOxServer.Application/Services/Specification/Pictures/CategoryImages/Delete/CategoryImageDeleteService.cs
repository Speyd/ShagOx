using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using static System.Net.Mime.MediaTypeNames;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Delete;
public class CategoryImageDeleteService
    : PictureDeleteService<CategoryImage>,
    ICategoryImageDeleteService
{
    private readonly CategoryImageInvalidationService _imageInvalid;


    public CategoryImageDeleteService(
        IRepository<CategoryImage> imageRepository,
        ICommandRepository<CategoryImage> imageCommand,
        CategoryImageValidator imageValidator,
        PictureValidator pictureValidator,
        CategoryImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CategoryImageDeleteService> logger
    )
        : base(imageRepository,
            imageCommand,
            imageValidator,
            pictureValidator,
            unitOfWork,
            logger)
    {
        _imageInvalid = imageInvalid;
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
                .Fail(EntityErrorResources.CategoryImageDeleteFailed);
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
                .Fail(EntityErrorResources.CategoryImageDeleteFailed);
        }
    }


    public override async Task InvalidateCache(
        CategoryImage image)
    {
        await _imageInvalid.InvalidateDeleteAsync(
            PictureCacheMapper.ToInfo(image));
    }
}
