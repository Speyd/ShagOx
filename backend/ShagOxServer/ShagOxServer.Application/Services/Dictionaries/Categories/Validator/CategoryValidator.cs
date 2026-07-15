using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
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

    public async Task<Result<bool>> ExistsByIdAsync(
       int categoryId)
    {
        if (!await _categoryExistsRepository.ExistsIdAsync(categoryId))
            return Result<bool>.NotFound("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
      int categoryId)
    {
        if (await _categoryExistsRepository.ExistsIdAsync(categoryId))
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
      string name,
      ProductType productType)
    {
        if (await _categoryExistsRepository.ExistsAsync(name, productType))
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameAsync(
       string name)
    {
        if (await _categoryExistsRepository.ExistsNameAsync(name))
            return Result<bool>.NotFound("Category");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByProductAsync(
       ProductType productType)
    {
        if (await _categoryExistsRepository.ExistsProductTypeAsync(productType))
            return Result<bool>.NotFound("Category");

        return Result<bool>.Success(true);
    }
}