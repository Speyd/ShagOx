using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Statistics;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Comments.Review;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;

/// <summary>
/// Read-only aggregates for the profile dashboard. Everything is computed in the
/// database (COUNT / SUM / AVG), nothing is loaded entity by entity.
/// Cancelled, rejected and refunded orders are not counted as sales or purchases.
/// </summary>
public class UserStatisticsRepository
    : IUserStatisticsRepository
{
    private const int TopSize = 5;

    private static readonly OrderStatus[] ClosedStatuses =
        [OrderStatus.Cancelled, OrderStatus.Rejected, OrderStatus.Refunded];

    private static readonly OrderStatus[] FinishedStatuses =
        [OrderStatus.Delivered, OrderStatus.Completed,
         OrderStatus.Cancelled, OrderStatus.Rejected, OrderStatus.Refunded];

    private readonly ReplicaDbContext _db;


    public UserStatisticsRepository(ReplicaDbContext db)
    {
        _db = db;
    }


    public async Task<UserStatisticsDto> GetAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        var seller = await GetSellerAsync(userId, cancellationToken);
        var buyer = await GetBuyerAsync(userId, cancellationToken);

        return new UserStatisticsDto(seller, buyer);
    }


    private async Task<SellerStatisticsDto> GetSellerAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var adverts = Advertisements().Where(a => a.OwnerId == userId);

        // Stock lives on the variants when an advertisement has them, otherwise on the advertisement.
        var stock = await adverts
            .Select(a => new
            {
                Units = a.Variants.Any(v => v.EntityStatus == EntityStatus.Active)
                    ? a.Variants
                        .Where(v => v.EntityStatus == EntityStatus.Active)
                        .Sum(v => v.Stock)
                    : a.Stock ?? 0
            })
            .ToListAsync(cancellationToken);

        var reviews = Reviews()
            .Where(r => adverts.Any(a => a.Id == r.CommentableEntityId));

        var grades = await reviews
            .GroupBy(r => r.Grade)
            .Select(g => new { Grade = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Grade, x => x.Count, cancellationToken);

        var reviewsCount = grades.Values.Sum();
        double? averageGrade = reviewsCount == 0
            ? null
            : Math.Round(grades.Sum(x => x.Key * x.Value) / (double)reviewsCount, 2);

        var favoritesCount = await Favorites()
            .CountAsync(f => adverts.Any(a => a.Id == f.AdvertisementId), cancellationToken);

        var soldItems = SoldItems()
            .Where(i => i.Advertisement.OwnerId == userId);

        var salesCount = await soldItems
            .Select(i => i.ParentId)
            .Distinct()
            .CountAsync(cancellationToken);

        var soldUnits = await soldItems
            .SumAsync(i => (int?)i.Quantity, cancellationToken) ?? 0;

        var revenue = await soldItems
            .GroupBy(i => new { i.Parent.Currency.Code, i.Parent.Currency.Symbol })
            .Select(g => new MoneyAmountDto(g.Key.Code, g.Key.Symbol, g.Sum(i => i.TotalPrice)))
            .ToListAsync(cancellationToken);

        var top = await GetTopAdvertisementsAsync(userId, cancellationToken);

        return new SellerStatisticsDto(
            stock.Count,
            stock.Count(x => x.Units <= 0),
            stock.Sum(x => Math.Max(x.Units, 0)),
            reviewsCount,
            averageGrade,
            Enumerable.Range(1, 5)
                .Reverse()
                .Select(grade => new GradeCountDto(grade, grades.GetValueOrDefault(grade)))
                .ToList(),
            favoritesCount,
            salesCount,
            soldUnits,
            revenue,
            top);
    }

    private async Task<List<TopAdvertisementDto>> GetTopAdvertisementsAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var soldItems = SoldItems();
        var favorites = Favorites();
        var reviews = Reviews();

        var rows = await Advertisements()
            .Where(a => a.OwnerId == userId)
            .Select(a => new
            {
                a.Id,
                a.Title,
                PictureUrl = a.Pictures
                    .Where(p => p.EntityStatus == EntityStatus.Active)
                    .OrderBy(p => p.Order)
                    .Select(p => p.Url)
                    .FirstOrDefault(),
                SoldUnits = soldItems
                    .Where(i => i.AdvertisementId == a.Id)
                    .Sum(i => (int?)i.Quantity) ?? 0,
                Favorites = favorites.Count(f => f.AdvertisementId == a.Id),
                Reviews = reviews.Count(r => r.CommentableEntityId == a.Id),
                AverageGrade = reviews
                    .Where(r => r.CommentableEntityId == a.Id)
                    .Average(r => (double?)r.Grade),
            })
            .OrderByDescending(x => x.SoldUnits)
            .ThenByDescending(x => x.Favorites)
            .ThenByDescending(x => x.Reviews)
            .ThenByDescending(x => x.Id)
            .Take(TopSize)
            .ToListAsync(cancellationToken);

        return rows
            .Select(x => new TopAdvertisementDto(
                x.Id,
                x.Title,
                x.PictureUrl,
                x.SoldUnits,
                x.Favorites,
                x.Reviews,
                x.AverageGrade is null ? null : Math.Round(x.AverageGrade.Value, 2)))
            .ToList();
    }

    private async Task<BuyerStatisticsDto> GetBuyerAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var orders = Orders().Where(o => o.OwnerId == userId);

        var ordersCount = await orders.CountAsync(cancellationToken);

        var activeOrders = await orders
            .CountAsync(o => !FinishedStatuses.Contains(o.OrderStatus), cancellationToken);

        var spent = await orders
            .Where(o => !ClosedStatuses.Contains(o.OrderStatus))
            .GroupBy(o => new { o.Currency.Code, o.Currency.Symbol })
            .Select(g => new MoneyAmountDto(g.Key.Code, g.Key.Symbol, g.Sum(o => o.TotalPrice)))
            .ToListAsync(cancellationToken);

        var favorites = await Favorites()
            .CountAsync(f => f.OwnerId == userId, cancellationToken);

        var reviewsWritten = await Reviews()
            .CountAsync(r => r.OwnerId == userId, cancellationToken);

        return new BuyerStatisticsDto(
            ordersCount,
            activeOrders,
            spent,
            favorites,
            reviewsWritten);
    }


    private IQueryable<Advertisement> Advertisements()
    {
        return _db.Advertisements
            .AsNoTracking()
            .WithStatusFilter(EntityStatus.ActiveAndHidden);
    }

    private IQueryable<ReviewComment> Reviews()
    {
        return _db.ReviewComments
            .AsNoTracking()
            .WithStatusFilter(EntityStatus.Active);
    }

    private IQueryable<Favorite> Favorites()
    {
        return _db.Favorites
            .AsNoTracking()
            .WithStatusFilter(EntityStatus.Active);
    }

    private IQueryable<Order> Orders()
    {
        return _db.Orders
            .AsNoTracking()
            .WithStatusFilter(EntityStatus.Active);
    }

    /// <summary>Order lines of orders that were not cancelled / rejected / refunded.</summary>
    private IQueryable<OrderItem> SoldItems()
    {
        return _db.OrderItems
            .AsNoTracking()
            .Where(i =>
                i.EntityStatus == EntityStatus.Active &&
                i.Parent.EntityStatus == EntityStatus.Active &&
                !ClosedStatuses.Contains(i.Parent.OrderStatus));
    }
}
