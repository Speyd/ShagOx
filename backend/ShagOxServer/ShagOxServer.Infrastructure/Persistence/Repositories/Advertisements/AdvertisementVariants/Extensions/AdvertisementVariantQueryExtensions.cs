using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
public static class AdvertisementVariantQueryExtensions
{
    public static IQueryable<AdvertisementVariant> WithIncludes(
        this IQueryable<AdvertisementVariant> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Advertisement)
                .ThenInclude(x => x.Currency);
    }
}
