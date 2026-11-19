using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Payments;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core.Extensions;
public static class PaymentQueryExtensions
{
    public static IQueryable<Payment> WithIncludes(
        this IQueryable<Payment> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Order)
                .ThenInclude(x => x.OrderItems)

            .Include(x => x.Order)
                .Include(x => x.Currency);
    }
}
