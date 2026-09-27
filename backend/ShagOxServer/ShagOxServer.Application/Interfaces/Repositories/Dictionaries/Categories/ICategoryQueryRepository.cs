using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryQueryRepository
    : IQueryRepository<Category, CategorySearchFilter>
{
    Task<PagedResult<Category>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination);
}