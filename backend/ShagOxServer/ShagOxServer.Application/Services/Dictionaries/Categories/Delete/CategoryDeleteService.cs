using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Delete;
public class CategoryDeleteService
    : BaseDeleteService<Category>
    , ICategoryDeleteService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;

    private readonly ICategoryImageDeleteService _imageDeleteService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryDeleteService> _logger;


    public CategoryDeleteService(
        IRepository<Category> categoryRepository,
        CategoryValidator categoryValidator,
        ICategoryImageDeleteService imageDeleteService,
        IUnitOfWork unitOfWork,
        ILogger<CategoryDeleteService> logger
    )
        : base(categoryRepository, categoryValidator, unitOfWork, logger)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _imageDeleteService = imageDeleteService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryValidator
            .GetByIdAsync(id, cancellationToken);

        if (!category.IsSuccess)
            return Result<DeleteResponse>.Fail(category.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _categoryRepository.Delete(category.Value!);

            if (category.Value!.CategoryImage is not null)
            {
                var imageResult = await _imageDeleteService
                    .DeleteInternalAsync(category.Value!.CategoryImage);

                if (!imageResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<DeleteResponse>
                        .Fail(imageResult.Error);
                }
            }

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete category. Id: {Id}",
               id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.CategoryDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                category.Value!.Id,
                DateTime.UtcNow
            )
      );
    }

    public override async Task<Result<bool>> SoftBodyDeleteAsync(
        Category entity,
        CancellationToken cancellationToken = default)
    {
        var body = await base.SoftBodyDeleteAsync(entity,
            cancellationToken);

        if (!body.IsSuccess)
            Result<bool>.Fail(body.Error);

        if(entity.CategoryImage is not null)
        {
            var imageDelete = await _imageDeleteService
                .SoftDeleteRecordAsync(entity.CategoryImage, 
                    cancellationToken);

            if (!imageDelete.IsSuccess)
                Result<bool>.Fail(imageDelete.Error);
        }

        return Result<bool>.Success(true);
    }
}
