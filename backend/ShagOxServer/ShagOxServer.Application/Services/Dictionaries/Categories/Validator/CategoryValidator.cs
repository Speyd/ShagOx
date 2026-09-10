using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
public class CategoryValidator
    : BaseValidator<Category>
{
    private readonly ICategoryExistsRepository _categoryExistsRepository;


    public CategoryValidator(
        IRepository<Category> categoryRepository,
        ICategoryExistsRepository categoryExistsRepository
    ) : base(categoryRepository, categoryExistsRepository)
    {
        _categoryExistsRepository = categoryExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
       string categoryName,
       int productTypeId)
    {
        if (!await _categoryExistsRepository.
                ExistsAsync(categoryName, productTypeId))
        {
            return Result<bool>
                .NotFound(typeof(Category));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
       string categoryCode,
       int productTypeId)
    {
        if (await _categoryExistsRepository.
                ExistsAsync(categoryCode, productTypeId))
        {
            return Result<bool>
                .AlreadyExists(typeof(Category));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByCodeAsync(
       string code)
    {
        if (!await _categoryExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>
                .NotFound(typeof(Category));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
       string code)
    {
        if (await _categoryExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>
                .AlreadyExists(typeof(Category));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByProductAsync(
       int productTypeId)
    {
        if (!await _categoryExistsRepository
            .ExistsByProductTypeAsync(productTypeId))
        {
            return Result<bool>
                .NotFound(typeof(Category));
        }

        return Result<bool>.Success(true);
    }
}