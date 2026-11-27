using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations.Extensions;
public static class ProductTypeTranslationQueryExtensions
{
    public static IQueryable<ProductTypeTranslation> WithIncludes(
       this IQueryable<ProductTypeTranslation> query,
        EntityStatus? status = null)
    {
        return query
           .Include(x => x.Translatable);
    }
}
