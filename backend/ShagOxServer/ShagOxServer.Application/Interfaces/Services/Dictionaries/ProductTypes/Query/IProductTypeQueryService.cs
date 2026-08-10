using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
public interface IProductTypeQueryService
    : IQueryService<ProductTypeDto>
{
    Task<Result<PagedResult<ProductTypeDto>>> Search(
        ProductTypeSearchFilter filter,
        PaginationParams pagination);
}