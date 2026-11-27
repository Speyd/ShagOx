using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
public static class StatusTranslationQueryExtensions
{
    public static IQueryable<StatusTranslation> WithIncludes(
        this IQueryable<StatusTranslation> query,
        EntityStatus? status = null)
    {
        return query
            .Include(x => x.Translatable);
    }
}
