using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Filters.Deliveries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Extensions;
public static class DeliveryProviderFilterExtensions
{
    public static IQueryable<DeliveryProvider> Filter(
        this IQueryable<DeliveryProvider> query,
        DeliveryProviderSearchFilter filter)
    {

        if (!string.IsNullOrWhiteSpace(filter?.Code))
        {

            query = query.Where(x =>
                EF.Functions.ILike(x.Code, $"%{filter.Code}%"));
        }

        return query;
    }
}
