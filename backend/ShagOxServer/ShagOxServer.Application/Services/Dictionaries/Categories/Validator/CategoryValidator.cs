using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
public class CategoryValidator
    : BaseValidator<Category>
{
    private readonly ICategoryExistsRepository _categoryExistsRepository;


    public CategoryValidator(
        IQueryRepository<Category> categoryRepository,
        ICategoryExistsRepository categoryExistsRepository
    ) : base(categoryRepository, categoryExistsRepository)
    {
        _categoryExistsRepository = categoryExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
       string categoryName,
       long productTypeId)
    {
        if (!await _categoryExistsRepository.
                ExistsAsync(categoryName, productTypeId))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Category);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
       string categoryCode,
       long productTypeId)
    {
        if (await _categoryExistsRepository.
                ExistsAsync(categoryCode, productTypeId))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Category);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByCodeAsync(
       string code)
    {
        if (!await _categoryExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Category);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
       string code)
    {
        if (await _categoryExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Category);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByProductAsync(
       long productTypeId)
    {
        if (!await _categoryExistsRepository
            .ExistsByProductTypeAsync(productTypeId))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Category);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByProductAsync(
       long productTypeId)
    {
        if (await _categoryExistsRepository
            .ExistsByProductTypeAsync(productTypeId))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Category);
        }

        return Result<bool>.Success(true);
    }
}
