using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Payments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core.Extensions;
public static class PaymentQueryExtensions
{
    public static IQueryable<Payment> WithIncludes(
        this IQueryable<Payment> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Order)
                .ThenInclude(x => x.OrderItems)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Pictures
                            .OrderBy(p => p.Order)
                            .Take(1))

            .Include(x => x.Order)
                .ThenInclude(x => x.OrderItems)
                    .ThenInclude(x => x.AdvertisementVariant)
                        .ThenInclude(x => x.Advertisement)
                            .ThenInclude(x => x.Pictures
                                .OrderBy(p => p.Order)
                                .Take(1))

            .Include(x => x.Order)
                .ThenInclude(x => x.Currency);
    }
}
