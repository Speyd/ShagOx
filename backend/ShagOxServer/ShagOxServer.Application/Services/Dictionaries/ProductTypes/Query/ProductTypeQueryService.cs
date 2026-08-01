using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Mapping;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Query;
public class ProductTypeQueryService 
    : IProductTypeQueryService
{
    private readonly IProductTypeQueryRepository _productTypeQueryRepository;


    public ProductTypeQueryService(
        IProductTypeQueryRepository productTypeQueryRepository)
    {
        _productTypeQueryRepository = productTypeQueryRepository;
    }


    public async Task<Result<ProductTypeDto>> GetByIdAsync(
        int id)
    {
        var productType = await _productTypeQueryRepository
            .GetByIdAsync(id);

        return productType.ToResult(ProductTypeMapper.ToDto);
    }

    public async Task<Result<PagedResult<ProductTypeDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var productTypes = await _productTypeQueryRepository
            .GetPagedAsync(pagination);

        return productTypes.ToResultPaged(ProductTypeMapper.ToDto);
    }

    public async Task<Result<PagedResult<ProductTypeDto>>> Search(
        ProductTypeSearchFilter filter,
        PaginationParams pagination)
    {
        var productTypes = await _productTypeQueryRepository
            .Search(filter, pagination);

        return productTypes.ToResultPaged(ProductTypeMapper.ToDto);
    }
}