using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryQueryRepository
    : ITranslatableQueryRepository<Category, CategorySearchFilter>
{
    Task<PagedResult<Category>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination);

    Task<List<CategoryCacheInfo>> GetCacheInfosByProductTypeAsync(
        long productTypeId);
}