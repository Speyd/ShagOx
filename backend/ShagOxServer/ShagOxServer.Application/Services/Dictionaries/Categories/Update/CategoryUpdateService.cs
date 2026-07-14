using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update;
public class CategoryUpdateService : ICategoryUpdateService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryValidator _validator;
    private readonly CategoryUpdateValidator _updateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CategoryUpdateService(
        ICategoryRepository categoryRepository,
        CategoryValidator validator,
        CategoryUpdateValidator updateValidator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _validator = validator;
        _updateValidator = updateValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CategoryUpdateResponse>> UpdateCategoryAsync(
        int categoryId,
        CategoryUpdateRequest request)
    {
        var category = await _validator.GetByIdAsync(categoryId);
        if(!category.IsSuccess)
            return Result<CategoryUpdateResponse>.Fail(category.Error ?? "");

        var changeValidator = _updateValidator.HasChangesValidator(category.Value!, request);
        if (!changeValidator.IsSuccess)
        {
            return Result<CategoryUpdateResponse>.Success(
                new CategoryUpdateResponse(
                DateTime.UtcNow,
                0
            ));
        }

        var existsValidator = await _validator.NotExistsAsync(
           changeValidator.Value!.name,
           changeValidator.Value!.productType
        );

        if (!existsValidator.IsSuccess)
            return Result<CategoryUpdateResponse>.Fail(existsValidator.Error ?? "");

        var updatedCount = CategoryUpdater.ApplyUpdates(category.Value!, request);
        var result = new CategoryUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CategoryUpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _categoryRepository.Update(category.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CategoryUpdateResponse>.Success(result);
    }
}