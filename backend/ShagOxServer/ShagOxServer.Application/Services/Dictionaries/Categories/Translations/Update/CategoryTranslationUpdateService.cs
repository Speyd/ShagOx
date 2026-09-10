using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Update;
public class CategoryTranslationUpdateService
    : BaseTranslationUpdateSerivce<Category, CategoryTranslation>,
    ICategoryTranslationUpdateService
{
    private readonly IRepository<CategoryTranslation> _categoryRepository;
    private readonly CategoryTranslationValidator _categoryTranslationValidator;


    private readonly IUnitOfWork _unitOfWork;


    public CategoryTranslationUpdateService(
        IRepository<CategoryTranslation> categoryRepository,
        CategoryTranslationValidator categoryTranslationValidator,
        CategoryValidator categoryValidator,
        IUnitOfWork unitOfWork
    ) : base(categoryValidator, categoryTranslationValidator)
    {
        _categoryRepository = categoryRepository;
        _categoryTranslationValidator = categoryTranslationValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        CategoryTranslationUpdateRequest request)
    {
        var category = await _categoryTranslationValidator
            .GetByIdAsync(statusTranslationId);

        if (!category.IsSuccess)
            return Result<UpdateResponse>.Fail(category.Error);


        var validation = await
             ValidateUpdatesAsync(category.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = CategoryTranslationUpdater
            .ApplyUpdates(category.Value!, request);

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