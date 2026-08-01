using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
public class ProductTypeValidator
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IProductTypeExistsRepository _productTypeExistsRepository;


    public ProductTypeValidator(
        IProductTypeRepository productTypeRepository,
        IProductTypeExistsRepository productTypeExistsRepository)
    {
        _productTypeRepository = productTypeRepository;
        _productTypeExistsRepository = productTypeExistsRepository;
    }


    public async Task<Result<ProductType>> GetByIdAsync(
        int productTypeId)
    {
        var productType = await _productTypeRepository
            .GetByIdAsync(productTypeId);

        if (productType is null)
            return Result<ProductType>.NotFound("ProductType");

        return Result<ProductType>.Success(productType);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
       int productTypeId)
    {
        if (!await _productTypeExistsRepository.ExistsByIdAsync(productTypeId))
            return Result<bool>.NotFound("ProductType");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
      int productTypeId)
    {
        if (await _productTypeExistsRepository.ExistsByIdAsync(productTypeId))
            return Result<bool>.AlreadyExists("ProductType");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameAsync(
       string name)
    {
        if (await _productTypeExistsRepository.ExistsByNameAsync(name))
            return Result<bool>.NotFound("ProductType");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
       string name)
    {
        if (await _productTypeExistsRepository.ExistsByNameAsync(name))
            return Result<bool>.AlreadyExists("ProductType");

        return Result<bool>.Success(true);
    }
}