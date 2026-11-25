using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
public static class CategoryQueryExtensions
{
    public static IQueryable<Category> WithIncludes(
       this IQueryable<Category> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.ProductType)
                .ThenInclude(x => x.Translations)
            .Include(x => x.Attributes)
            .Include(x => x.CategoryImage)
            .Include(x => x.Advertisements);
    }

    public static IQueryable<CategoryCacheContext> SelectCacheInfo(
        this IQueryable<Category> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new CategoryCacheContext(
            x.Id,
            x.ProductTypeId,
            status));
    }
}
