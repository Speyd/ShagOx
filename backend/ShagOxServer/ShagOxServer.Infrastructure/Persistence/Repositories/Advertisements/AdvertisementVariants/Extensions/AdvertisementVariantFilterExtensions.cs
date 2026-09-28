using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
public static class AdvertisementVariantFilterExtensions
{
    public static IQueryable<AdvertisementVariant> Filter(
        this IQueryable<AdvertisementVariant> query,
        AdvertisementVariantSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.AdvertisementId.HasValue)
        {
            query = query.Where(x =>
                x.AdvertisementId == filter.AdvertisementId);
        }

        if (filter.Price.HasValue)
        {
            query = query.Where(x =>
                 x.Price == filter.Price);
        }

        if (filter.Stock.HasValue)
        {
            query = query.Where(x =>
                 x.Stock == filter.Stock);
        }
       
        if (filter.Attributes is not null)
        {
            JsonElement v;

            query = query.Where(x =>
                filter.Attributes.All(a =>
                    x.Attributes.RootElement.TryGetProperty(a, out v)));
        }

        return query;
    }
}