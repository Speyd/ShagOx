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
public partial class UserStatisticsRepository
    : IUserStatisticsRepository
{
    private static readonly OrderStatus[] ClosedStatuses =
        [OrderStatus.Cancelled, OrderStatus.Rejected, OrderStatus.Refunded];

    private static readonly OrderStatus[] FinishedStatuses =
        [OrderStatus.Delivered, OrderStatus.Completed,
         OrderStatus.Cancelled, OrderStatus.Rejected, OrderStatus.Refunded];

    private readonly ReplicaDbContext _db;


    public UserStatisticsRepository(
        ReplicaDbContext db)
    {
        _db = db;
    }


    public async Task<UserStatisticsDto> GetByUserAsync(
        long userId,
        UserStatisticQueryOptions options,
        CancellationToken cancellationToken = default)
    {
        var seller = await GetSellerAsync(
            userId, 
            options, 
            cancellationToken);

        var buyer = await GetBuyerAsync(
            userId, 
            cancellationToken);

        return new UserStatisticsDto(seller, buyer);
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
