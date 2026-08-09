using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update;
public class CategoryUpdateService 
    : ICategoryUpdateService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;
    private readonly CategoryUpdateValidator _categoryUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CategoryUpdateService(
        IRepository<Category> categoryRepository,
        CategoryValidator categoryValidator,
        CategoryUpdateValidator categoryUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _categoryUpdateValidator = categoryUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int categoryId,
        CategoryUpdateRequest request)
    {
        var category = await _categoryValidator.GetByIdAsync(categoryId);
        if(!category.IsSuccess)
            return Result<UpdateResponse>.Fail(category.Error);

        if (request.Name is not null)
        {
            var nameValidator = await _categoryValidator.GetByIdAsync(categoryId);
            if (!category.IsSuccess)
                return Result<UpdateResponse>.Fail(category.Error);
        }

        var changeValidator = _categoryUpdateValidator
            .HasChangesValidator(category.Value!, request);

        if (!changeValidator.IsSuccess)
        {
            return Result<UpdateResponse>.Success(
                new UpdateResponse(
                    0,
                    DateTime.UtcNow
            ));
        }

        var existsValidator = await _categoryValidator.NotExistsAsync(
           changeValidator.Value!.name,
           changeValidator.Value!.productTypeId
        );

        if (!existsValidator.IsSuccess)
            return Result<UpdateResponse>.Fail(existsValidator.Error);

        var updatedCount = CategoryUpdater.ApplyUpdates(category.Value!, request);
        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

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

        return Result<UpdateResponse>.Success(result);
    }
}