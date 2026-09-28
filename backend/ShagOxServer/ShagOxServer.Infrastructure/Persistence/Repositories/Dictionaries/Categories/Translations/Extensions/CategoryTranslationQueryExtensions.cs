using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations.Extensions;
public static class CategoryTranslationQueryExtensions
{
    public static IQueryable<CategoryTranslation> WithIncludes(
       this IQueryable<CategoryTranslation> query)
    {
        return query
           .Include(x => x.Translatable);
    }
}
