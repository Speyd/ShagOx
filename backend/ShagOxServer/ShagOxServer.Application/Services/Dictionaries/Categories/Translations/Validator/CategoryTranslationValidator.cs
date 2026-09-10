using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
public class CategoryTranslationValidator
    : BaseTranslationValidator<CategoryTranslation>
{
    private readonly ICategoryTranslationExistsRepository _categoryExistsRepository;


    public CategoryTranslationValidator(
        IRepository<CategoryTranslation> categoryRepository,
        ICategoryTranslationExistsRepository categoryExistsRepository
    ) : base(categoryRepository, categoryExistsRepository)
    {
        _categoryExistsRepository = categoryExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _categoryExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .NotFound(typeof(CategoryTranslation));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _categoryExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .AlreadyExists(typeof(CategoryTranslation));
        }

        return Result<bool>.Success(true);
    }
}