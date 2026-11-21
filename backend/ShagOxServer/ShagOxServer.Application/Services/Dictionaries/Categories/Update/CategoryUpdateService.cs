using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update;
public class CategoryUpdateService
    : BaseUpdateService<Category>,
    ICategoryUpdateService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;
    private readonly CategoryUpdateValidator _categoryUpdateValidator;

    private readonly ICategoryImageUpdateService _imageUploadService;
    private readonly ICategoryImageCreateService _imageCreateService;
    private readonly CategoryImageValidator _imageValidator;

    private readonly CategoryInvalidationService _categoryInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryUpdateService> _logger;


    public CategoryUpdateService(
        IRepository<Category> categoryRepository,
        CategoryValidator categoryValidator,
        ICategoryDeleteService categoryDeleteValidator,
        CategoryUpdateValidator categoryUpdateValidator,
        ICategoryImageUpdateService imageUploadService,
        ICategoryImageCreateService imageCreateService,
        CategoryImageValidator imageValidator,
        CategoryInvalidationService categoryInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CategoryUpdateService> logger
    )
        : base(categoryDeleteValidator, categoryValidator, logger, unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _categoryUpdateValidator = categoryUpdateValidator;
        _imageUploadService = imageUploadService;
        _imageCreateService = imageCreateService;
        _imageValidator = imageValidator;
        _categoryInvalid = categoryInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long categoryId,
        CategoryUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        // Read model (replica, with includes) — used only for validation and to
        // find the current picture. It must never be passed to Update(): its graph
        // contains every advertisement of the category.
        var category = await _categoryValidator
            .GetByIdAsync(categoryId, cancellationToken);

        if(!category.IsSuccess)
            return Result<UpdateResponse>.Fail(category.Error);

        var changeValidator = _categoryUpdateValidator
            .HasChangesValidator(category.Value!, request);

        var hasFieldChanges = changeValidator.IsSuccess;
        var hasPicture = request.Picture is not null;

        if (!hasFieldChanges && !hasPicture)
        {
            return Result<UpdateResponse>.Success(
                new UpdateResponse(0, DateTime.UtcNow));
        }

        if (hasFieldChanges)
        {
            var validation = await ValidateUpdatesAsync(
                changeValidator.Value!,
                request,
                cancellationToken);

            if (!validation.IsSuccess)
                return Result<UpdateResponse>.Fail(validation.Error);
        }

        // Tracked copy from the primary database: only Code / ProductTypeId change.
        var tracked = await _categoryValidator
            .GetByIdAttachAsync(categoryId, cancellationToken);

        if (!tracked.IsSuccess)
            return Result<UpdateResponse>.Fail(tracked.Error);

        var oldProductTypeId = tracked.Value!.ProductTypeId;
        var updatedCount = 0;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (hasFieldChanges)
            {
                updatedCount += CategoryUpdater.ApplyUpdates(
                    tracked.Value!,
                    request with { Picture = null });

                await _unitOfWork.SaveChangesAsync();
            }

            if (hasPicture)
            {
                var pictureResult = await SavePictureAsync(
                    categoryId,
                    category.Value!.CategoryImage?.Id,
                    request.Picture!,
                    cancellationToken);

                if (!pictureResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<UpdateResponse>
                        .Fail(pictureResult.Error);
                }

                updatedCount++;
            }

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating category." +
                " Id: {Id}",
                categoryId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to update category. Id: {Id}",
               categoryId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.CategoryUpdateFailed);
        }

        await _categoryInvalid.InvalidateUpdateAsync(
            CategoryCacheMapper.ToInfo(tracked.Value!));

        // Moving to another product type: lists of the old type are stale too.
        if (oldProductTypeId != tracked.Value!.ProductTypeId)
        {
            await _categoryInvalid.InvalidateUpdateAsync(
                CategoryCacheMapper.ToInfo(category.Value!));
        }

        return Result<UpdateResponse>.Success(
            new UpdateResponse(updatedCount, DateTime.UtcNow));
    }

    /// <summary>
    /// Replaces the current picture or creates the first one. The existing picture
    /// is loaded from the primary database (not from the replica graph, whose
    /// Owner navigation would drag the whole category into the change tracker).
    /// </summary>
    private async Task<Result<bool>> SavePictureAsync(
        long categoryId,
        long? currentImageId,
        IFormFile picture,
        CancellationToken cancellationToken)
    {
        if (currentImageId is long imageId)
        {
            var image = await _imageValidator
                .GetByIdAttachAsync(imageId, cancellationToken);

            if (image.IsSuccess)
            {
                var updated = await _imageUploadService.UpdateInternalAsync(
                    image.Value!,
                    new PictureUpdateRequest(null, picture),
                    cancellationToken);

                return updated.IsSuccess
                    ? Result<bool>.Success(true)
                    : Result<bool>.Fail(updated.Error);
            }
        }

        var created = await _imageCreateService.CreateInternalAsync(
            new PictureCreateRequest(categoryId, picture),
            cancellationToken);

        return created.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Fail(created.Error);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        (string name, long productTypeId) changeValidator,
        CategoryUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var existsValidator = await _categoryValidator.NotExistsAsync(
            changeValidator.name,
            changeValidator.productTypeId,
            cancellationToken
        );

        return existsValidator;
    }
}
