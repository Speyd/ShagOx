using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Delete;
public class CategoryDeleteService
    : BaseDeleteService<Category>
    , ICategoryDeleteService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;

    private readonly CategoryInvalidationService _categoryInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryDeleteService> _logger;


    public CategoryDeleteService(
        IRepository<Category> categoryRepository,
        CategoryValidator categoryValidator,
        CategoryInvalidationService categoryInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CategoryDeleteService> logger
    )
        : base(categoryValidator, unitOfWork, logger)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _categoryInvalid = categoryInvalid;
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

        await InvalidateCache(category.Value!);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                category.Value!.Id,
                DateTime.UtcNow
            )
      );
    }

    public override async Task InvalidateCache(
        Category entity)
    {
        await _categoryInvalid.InvalidateDeleteAsync(
            CategoryCacheMapper.ToInfo(entity));
    }
}
