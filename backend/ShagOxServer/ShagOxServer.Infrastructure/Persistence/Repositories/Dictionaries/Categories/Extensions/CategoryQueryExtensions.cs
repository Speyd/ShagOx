using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Domain.Base.Enum;
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
            .Include(x => x.Advertisements);
    }

    public static IQueryable<CategoryCacheInfo> SelectCacheInfo(
        this IQueryable<Category> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new CategoryCacheInfo(
            x.Id,
            x.ProductTypeId,
            status));
    }
}
