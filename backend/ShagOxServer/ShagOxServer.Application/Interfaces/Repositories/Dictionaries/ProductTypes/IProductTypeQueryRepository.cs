using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
public interface IProductTypeQueryRepository
{
    Task<ProductType?> GetByIdAsync(int id);

    Task<PagedResult<ProductType>> GetPagedAsync(
        PaginationParams pagination);

    Task<PagedResult<ProductType>> Search(
       ProductTypeSearchFilter filter,
       PaginationParams pagination);
}