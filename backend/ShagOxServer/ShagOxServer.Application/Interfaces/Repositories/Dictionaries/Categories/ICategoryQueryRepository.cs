using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryQueryRepository
{
    Task<Category?> GetByIdAsync(int id);

    Task<PagedResult<Category>> GetPagedAsync(
        PaginationParams pagination);

    Task<Category?> GetByNameAsync(string name);

    Task<PagedResult<Category>> GetByProductTypeAsync(
        ProductType type,
        PaginationParams pagination);

    Task<PagedResult<Category>> Search(
       CategorySearchFilter filter,
       PaginationParams pagination);
}