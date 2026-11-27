using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations.Extensions;
public static class RegionTranslationQueryExtensions
{
    public static IQueryable<RegionTranslation> WithIncludes(
       this IQueryable<RegionTranslation> query,
        EntityStatus? status = null)
    {
        return query
           .Include(x => x.Translatable);
    }
}
