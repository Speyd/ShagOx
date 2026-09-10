using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Delete;
public class CategoryTranslationDeleteService
    : ICategoryTranslationDeleteService
{
    private readonly IRepository<CategoryTranslation> _categoryRepository;
    private readonly CategoryTranslationValidator _categoryValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CategoryTranslationDeleteService(
        IRepository<CategoryTranslation> categoryRepository,
        CategoryTranslationValidator categoryValidator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var category = await _categoryValidator
            .GetByIdAsync(id);

        if (!category.IsSuccess)
            return Result<DeleteResponse>.Fail(category.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _categoryRepository.Delete(category.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               category.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}