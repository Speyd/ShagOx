using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
public interface IProductTypeQueryRepository
    : IQueryRepository<ProductType>
{
    Task<PagedResult<ProductType>> Search(
       ProductTypeSearchFilter filter,
       PaginationParams pagination);
}