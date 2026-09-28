using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations.Extensions;
public static class CityTranslationQueryExtensions
{
    public static IQueryable<CityTranslation> WithIncludes(
       this IQueryable<CityTranslation> query)
    {
        return query
           .Include(x => x.Translatable);
    }
}