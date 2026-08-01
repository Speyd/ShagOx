using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
public interface IProductTypeQueryService
{
    Task<Result<ProductTypeDto>> GetByIdAsync(int id);

    Task<Result<PagedResult<ProductTypeDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<PagedResult<ProductTypeDto>>> Search(
        ProductTypeSearchFilter filter,
        PaginationParams pagination);
}