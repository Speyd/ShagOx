using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
public class ProductTypeValidator
    : BaseValidator<ProductType>
{
    private readonly IProductTypeExistsRepository _productTypeExistsRepository;


    public ProductTypeValidator(
        IRepository<ProductType> repository,
        IQueryRepository<ProductType> productTypeRepository,
        IProductTypeExistsRepository productTypeExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, productTypeRepository, productTypeExistsRepository, cache)
    {
        _productTypeExistsRepository = productTypeExistsRepository;
    }


    public async Task<Result<bool>> ExistsByCodeAsync(
       string code,
       CancellationToken cancellationToken = default)
    {
        if (!await _productTypeExistsRepository
            .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.ProductType);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
       string code,
       CancellationToken cancellationToken = default)
    {

        if (await _productTypeExistsRepository
            .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.ProductType);
        }

        return Result<bool>.Success(true);
    }
}
