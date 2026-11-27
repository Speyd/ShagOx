using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.CategoryImages.Extensions;
public static class CategoryImageQueryExtension
{
    public static IQueryable<CategoryImage> WithIncludes(
      this IQueryable<CategoryImage> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Owner);
    }
}
