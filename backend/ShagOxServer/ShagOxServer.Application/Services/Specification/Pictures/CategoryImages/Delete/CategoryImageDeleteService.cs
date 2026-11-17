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
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Delete;
public class CategoryImageDeleteService
    : PictureDeleteService<CategoryImage>,
    ICategoryImageDeleteService
{
    private readonly CategoryImageInvalidationService _avatarInvalid;


    public CategoryImageDeleteService(
        IRepository<CategoryImage> avatarRepository,
        CategoryImageValidator avatarValidator,
        PictureValidator pictureValidator,
        CategoryImageInvalidationService avatarInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CategoryImageDeleteService> logger
    )
        : base(avatarRepository,
            avatarValidator,
            pictureValidator,
            unitOfWork,
            logger)
    {
        _avatarInvalid = avatarInvalid;
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
        CategoryImage avatar)
    {
        await _avatarInvalid.InvalidateDeleteAsync(
            PictureCacheMapper.ToInfo(avatar));
    }
}
