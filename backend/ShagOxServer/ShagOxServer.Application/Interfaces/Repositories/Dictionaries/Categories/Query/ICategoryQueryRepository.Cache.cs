using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
public partial interface ICategoryQueryRepository
    : ISearchTranslatableRepository<Category, CategorySearchFilter>
{
    Task<List<CategoryCacheContext>> GetCacheInfosByProductTypeAsync(
        long productTypeId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
