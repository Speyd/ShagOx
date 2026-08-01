using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
public class CategoryValidator
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryExistsRepository _categoryExistsRepository;


    public CategoryValidator(
        ICategoryRepository categoryRepository,
        ICategoryExistsRepository categoryExistsRepository)
    {
        _categoryRepository = categoryRepository;
        _categoryExistsRepository = categoryExistsRepository;
    }


    public async Task<Result<Category>> GetByIdAsync(
        int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
            return Result<Category>.NotFound("Category");

        return Result<Category>.Success(category);
    }

    public async Task<Result<bool>> ExistsAsync(
       string categoryName,
       int productTypeId)
    {
        if (!await _categoryExistsRepository.
                ExistsAsync(categoryName, productTypeId))
        {
            return Result<bool>.NotFound("Category");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
       string categoryName,
       int productTypeId)
    {
        if (await _categoryExistsRepository.
                ExistsAsync(categoryName, productTypeId))
        {
            return Result<bool>.AlreadyExists("Category");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
       int categoryId)
    {
        if (!await _categoryExistsRepository.ExistsByIdAsync(categoryId))
            return Result<bool>.NotFound("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
      int categoryId)
    {
        if (await _categoryExistsRepository.ExistsByIdAsync(categoryId))
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameAsync(
       string name)
    {
        if (!await _categoryExistsRepository.ExistsByNameAsync(name))
            return Result<bool>.NotFound("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
       string name)
    {
        if (await _categoryExistsRepository.ExistsByNameAsync(name))
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByProductAsync(
       int productTypeId)
    {
        if (!await _categoryExistsRepository.ExistsByProductTypeAsync(productTypeId))
            return Result<bool>.NotFound("Category");

        return Result<bool>.Success(true);
    }
}