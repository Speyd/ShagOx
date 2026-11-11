using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Domain.Filters.Deliveries.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Translations.Extensions;
public static class DeliveryProviderTranslationFilterExtensions
{
    public static IQueryable<DeliveryProviderTranslation> Filter(
        this IQueryable<DeliveryProviderTranslation> query,
        DeliveryProviderTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.DeliveryProviderCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Code, $"%{filter.DeliveryProviderCode}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Language))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Language, $"%{filter.Language}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Name, $"%{filter.Name}%"));
        }

        return query;
    }
}
