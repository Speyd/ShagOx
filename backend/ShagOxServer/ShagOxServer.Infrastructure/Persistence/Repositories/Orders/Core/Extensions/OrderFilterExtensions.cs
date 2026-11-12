using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.Core;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Extensions;
public static class OrderFilterExtensions
{
    public static IQueryable<Order> Filter(
        this IQueryable<Order> query,
        OrderSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.OwnerId.HasValue)
            query = query.Where(x => x.OwnerId == filter.OwnerId);

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var term = filter.SearchTerm.Trim();

            query = query.Where(x =>
                EF.Functions.ILike(x.OrderNumber, $"%{term}%") ||
                EF.Functions.ILike(x.FirstName, $"%{term}%") ||
                EF.Functions.ILike(x.LastName, $"%{term}%") ||
                EF.Functions.ILike(x.Phone, $"%{term}%") ||
                EF.Functions.ILike(x.Email, $"%{term}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.OrderNumber))
            query = query.Where(x =>
                EF.Functions.ILike(x.OrderNumber, $"%{filter.OrderNumber.Trim()}%"));



        if (!string.IsNullOrWhiteSpace(filter.FirstName))
            query = query.Where(x =>
                EF.Functions.ILike(x.FirstName, $"%{filter.FirstName.Trim()}%"));

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            query = query.Where(x =>
                EF.Functions.ILike(x.LastName, $"%{filter.LastName.Trim()}%"));

        if (!string.IsNullOrWhiteSpace(filter.Phone))
            query = query.Where(x =>
                EF.Functions.ILike(x.Phone, $"%{filter.Phone.Trim()}%"));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(x =>
                EF.Functions.ILike(x.Email, $"%{filter.Email.Trim()}%"));



        if (filter.MinTotalPrice.HasValue)
            query = query.Where(x => x.TotalPrice >= filter.MinTotalPrice.Value);

        if (filter.MaxTotalPrice.HasValue)
            query = query.Where(x => x.TotalPrice <= filter.MaxTotalPrice.Value);

        if (filter.CurrencyId.HasValue)
            query = query.Where(x => x.CurrencyId == filter.CurrencyId.Value);



        if (filter.DeliveryType.HasValue)
            query = query.Where(x => x.DeliveryType == filter.DeliveryType.Value);

        if (filter.DeliveryProviderId.HasValue)
            query = query.Where(x => x.DeliveryProviderId == filter.DeliveryProviderId.Value);

        if (!string.IsNullOrWhiteSpace(filter.DeliveryTrackingNumber))
        {
            var trackingNumber = filter.DeliveryTrackingNumber.Trim();

            query = query.Where(x =>
                x.DeliveryTrackingNumber != null &&
                EF.Functions.ILike(x.DeliveryTrackingNumber, $"%{trackingNumber}%"));
        }

        if (filter.DeliveryPointType.HasValue)
            query = query.Where(x => x.DeliveryPointType == filter.DeliveryPointType.Value);



        if (filter.PaymentMethod.HasValue)
            query = query.Where(x => x.PaymentMethod == filter.PaymentMethod.Value);

        if (filter.PaymentStatus.HasValue)
            query = query.Where(x => x.PaymentStatus == filter.PaymentStatus.Value);



        if (filter.OrderStatus.HasValue)
            query = query.Where(x => x.OrderStatus == filter.OrderStatus.Value);



        if (filter.CreatedFrom.HasValue)
            query = query.Where(x => x.CreatedAt >= filter.CreatedFrom.Value);

        if (filter.CreatedTo.HasValue)
            query = query.Where(x => x.CreatedAt < filter.CreatedTo.Value);

        if (filter.PaidFrom.HasValue)
            query = query.Where(x => x.PaidAt >= filter.PaidFrom.Value);

        if (filter.PaidTo.HasValue)
            query = query.Where(x => x.PaidAt < filter.PaidTo.Value);

        if (filter.ShippedFrom.HasValue)
            query = query.Where(x => x.ShippedAt >= filter.ShippedFrom.Value);

        if (filter.ShippedTo.HasValue)
            query = query.Where(x => x.ShippedAt < filter.ShippedTo.Value);

        if (filter.DeliveredFrom.HasValue)
            query = query.Where(x => x.DeliveredAt >= filter.DeliveredFrom.Value);

        if (filter.DeliveredTo.HasValue)
            query = query.Where(x => x.DeliveredAt < filter.DeliveredTo.Value);

        return query;
    }
}
