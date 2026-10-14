using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
public partial interface ICategoryQueryRepository
    : ISearchTranslatableRepository<Category, CategorySearchFilter>
{
    Task<PagedResult<Category>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination);
}
