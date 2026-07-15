using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update;
public class CategoryUpdateService : ICategoryUpdateService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryValidator _categoryValidator;
    private readonly CategoryUpdateValidator _categoryUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CategoryUpdateService(
        ICategoryRepository categoryRepository,
        CategoryValidator categoryValidator,
        CategoryUpdateValidator categoryUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _categoryUpdateValidator = categoryUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CategoryUpdateResponse>> UpdateAsync(
        int categoryId,
        CategoryUpdateRequest request)
    {
        var category = await _categoryValidator.GetByIdAsync(categoryId);
        if(!category.IsSuccess)
            return Result<CategoryUpdateResponse>.Fail(category.Error ?? "");

        var changeValidator = _categoryUpdateValidator
            .HasChangesValidator(category.Value!, request);

        if (!changeValidator.IsSuccess)
        {
            return Result<CategoryUpdateResponse>.Success(
                new CategoryUpdateResponse(
                DateTime.UtcNow,
                0
            ));
        }

        var existsValidator = await _categoryValidator.NotExistsAsync(
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