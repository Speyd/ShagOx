using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Query;
public partial class CategoryQueryRepository
    : QueryRepository<Category, CategorySearchFilter>,
      ICategoryQueryRepository
{
    protected override IQueryable<Category> ApplyIncludes(
        IQueryable<Category> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Category> ApplyFilter(
        IQueryable<Category> query,
        CategorySearchFilter filter)
    {
        return query.Filter(filter);
    }
}