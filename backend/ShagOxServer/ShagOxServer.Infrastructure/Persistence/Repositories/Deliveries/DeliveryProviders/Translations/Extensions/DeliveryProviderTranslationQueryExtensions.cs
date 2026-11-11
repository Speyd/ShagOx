using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Translations.Extensions;
public static class DeliveryProviderTranslationQueryExtensions
{
    public static IQueryable<DeliveryProviderTranslation> WithIncludes(
        this IQueryable<DeliveryProviderTranslation> query)
    {
        return query
            .Include(x => x.Translatable);
    }
}
