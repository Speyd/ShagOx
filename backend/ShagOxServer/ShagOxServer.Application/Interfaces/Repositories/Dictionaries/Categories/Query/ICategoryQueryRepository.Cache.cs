using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
public partial interface ICategoryQueryRepository
    : ISearchTranslatableRepository<Category, CategorySearchFilter>
{
    Task<List<CategoryCacheInfo>> GetCacheInfosByProductTypeAsync(
        long productTypeId);
}
