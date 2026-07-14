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


    public async Task<Result<Category>> GetCategoryValidator(
        int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
            return Result<Category>.NotFound("Category");

        return Result<Category>.Success(category);
    }

    public async Task<Result<bool>> ExistsCategoryValidator(
       int categoryId)
    {
        var category = await _categoryExistsRepository.ExistsIdAsync(categoryId);
        if (category)
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(category);
    }

    public async Task<Result<bool>> ExistsCategoryValidator(
      string name,
      ProductType productType)
    {
        var category = await _categoryExistsRepository.ExistsAsync(name, productType);
        if (category)
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(category);
    }

    public async Task<Result<bool>> ExistsCategoryByNameValidator(
       string name)
    {
        var category = await _categoryExistsRepository.ExistsNameAsync(name);
        if (category)
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(category);
    }

    public async Task<Result<bool>> ExistsCategoryByProductValidator(
       ProductType productType)
    {
        var category = await _categoryExistsRepository.ExistsProductTypeAsync(productType);
        if (category)
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(category);
    }
}