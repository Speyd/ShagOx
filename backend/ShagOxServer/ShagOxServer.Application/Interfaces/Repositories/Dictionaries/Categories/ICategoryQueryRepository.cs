using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryQueryRepository
    : IQueryRepository<Category>
{
    Task<PagedResult<Category>> GetByProductTypeAsync(
        int productTypeId,
        PaginationParams pagination);

    Task<PagedResult<Category>> Search(
       CategorySearchFilter filter,
       PaginationParams pagination);
}