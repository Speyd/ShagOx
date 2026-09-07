using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
public class ProductTypeValidator
    : BaseValidator<ProductType>
{
    private readonly IProductTypeExistsRepository _productTypeExistsRepository;


    public ProductTypeValidator(
        IRepository<ProductType> productTypeRepository,
        IProductTypeExistsRepository productTypeExistsRepository
    ) : base(productTypeRepository, productTypeExistsRepository)
    {
        _productTypeExistsRepository = productTypeExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
       string name)
    {
        if (await _productTypeExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .NotFound(typeof(ProductType));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
       string name)
    {

        if (await _productTypeExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .AlreadyExists(typeof(ProductType));
        }

        return Result<bool>.Success(true);
    }
}