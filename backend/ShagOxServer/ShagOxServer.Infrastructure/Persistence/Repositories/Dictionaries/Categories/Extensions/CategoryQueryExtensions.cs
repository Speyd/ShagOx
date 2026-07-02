using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
public static class CategoryQueryExtensions
{
    public static IQueryable<Category> WithIncludes(
       this IQueryable<Category> query)
    {
        return query
           .Include(x => x.Attributes)
           .Include(x => x.Advertisements);
    }
}
