using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.CategoryImages.Extensions;
public static class CategoryImageFilterExtension
{
    public static IQueryable<CategoryImage> Filter(
        this IQueryable<CategoryImage> query,
        CategoryImageSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.PublicId))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.PublicId, $"%{filter.PublicId}%"));
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(x =>
                x.OwnerId == filter.CategoryId);
        }

        return query;
    }
}
