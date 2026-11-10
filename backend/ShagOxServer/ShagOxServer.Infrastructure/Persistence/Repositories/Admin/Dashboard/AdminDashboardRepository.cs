using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Admin.Dashboard;
using ShagOxServer.Application.Interfaces.Repositories.Admin.Dashboard;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Admin.Dashboard;
public class AdminDashboardRepository
    : IAdminDashboardRepository
{
    private const int GrowthMonths = 12;
    private const int TopCategories = 8;
    private const int ListSize = 5;
    private const int LowStockThreshold = 2;
    private const string OtherCategoriesName = "Інші";

    private readonly ReplicaDbContext _db;


    public AdminDashboardRepository(ReplicaDbContext db)
    {
        _db = db;
    }


    public async Task<AdminDashboardDto> GetDashboardAsync(
        string language,
        CancellationToken cancellationToken = default)
    {
        var totals = await GetTotalsAsync(cancellationToken);
        var growth = await GetGrowthAsync(cancellationToken);
        var byCategory = await GetAdvertisementsByCategoryAsync(language, cancellationToken);
        var grades = await GetGradesAsync(cancellationToken);
        var latestUsers = await GetLatestUsersAsync(cancellationToken);
        var latestAdvertisements = await GetLatestAdvertisementsAsync(cancellationToken);
        var topSellers = await GetTopSellersAsync(cancellationToken);
        var lowStock = await GetLowStockAsync(cancellationToken);

        return new AdminDashboardDto(
            totals,
            growth,
            byCategory,
            grades,
            latestUsers,
            latestAdvertisements,
            topSellers,
            lowStock);
    }

    private async Task<DashboardTotalsDto> GetTotalsAsync(
        CancellationToken cancellationToken)
    {
        var last30Days = DateTime.UtcNow.AddDays(-30);

        var users = Users();
        var ads = Advertisements();
        var reviews = Reviews();

        var usersTotal = await users.CountAsync(cancellationToken);
        var usersLast30 = await users
            .CountAsync(x => x.RegisteredAt >= last30Days, cancellationToken);

        var adsTotal = await ads.CountAsync(cancellationToken);
        var adsLast30 = await ads
            .CountAsync(x => x.CreatedAt >= last30Days, cancellationToken);
        var soldTotal = await ads
            .CountAsync(x => x.SoldAt != null, cancellationToken);

        var totalStock = await ads
            .SelectMany(x => x.Variants)
            .Where(v => v.EntityStatus != EntityStatus.Deleted)
            .SumAsync(v => v.Stock, cancellationToken);

        var reviewsTotal = await reviews.CountAsync(cancellationToken);
        var averageRating = await reviews
            .AverageAsync(x => (double?)x.Grade, cancellationToken) ?? 0;

        var categories = await _db.Categories
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);
        var productTypes = await _db.ProductTypes
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);
        var dictionaries = await _db.AttributeDictionaries
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);
        var cities = await _db.Cities
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);

        return new DashboardTotalsDto(
            usersTotal,
            usersLast30,
            adsTotal,
            adsLast30,
            soldTotal,
            totalStock,
            reviewsTotal,
            averageRating,
            categories,
            productTypes,
            dictionaries,
            cities);
    }

    private async Task<List<DashboardGrowthDto>> GetGrowthAsync(
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var from = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddMonths(-(GrowthMonths - 1));

        var users = await Users()
            .Where(x => x.RegisteredAt >= from)
            .GroupBy(x => new { x.RegisteredAt.Year, x.RegisteredAt.Month })
            .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
            .ToListAsync(cancellationToken);

        var ads = await Advertisements()
            .Where(x => x.CreatedAt >= from)
            .GroupBy(x => new { x.CreatedAt.Year, x.CreatedAt.Month })
            .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
            .ToListAsync(cancellationToken);

        return Enumerable.Range(0, GrowthMonths)
            .Select(offset => from.AddMonths(offset))
            .Select(month => new DashboardGrowthDto(
                $"{month.Year}-{month.Month:D2}",
                users.FirstOrDefault(x => x.Year == month.Year && x.Month == month.Month)?.Count ?? 0,
                ads.FirstOrDefault(x => x.Year == month.Year && x.Month == month.Month)?.Count ?? 0))
            .ToList();
    }

    private async Task<List<DashboardCategoryDto>> GetAdvertisementsByCategoryAsync(
        string language,
        CancellationToken cancellationToken)
    {
        var counts = await Advertisements()
            .GroupBy(x => x.CategoryId)
            .Select(g => new { CategoryId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToListAsync(cancellationToken);

        var top = counts.Take(TopCategories).ToList();
        var topIds = top.Select(x => x.CategoryId).ToList();

        var names = await _db.Categories
            .Where(c => topIds.Contains(c.Id))
            .Select(c => new
            {
                c.Id,
                c.Code,
                Name = c.Translations
                    .Where(t => t.Language == language)
                    .Select(t => t.Name)
                    .FirstOrDefault()
            })
            .ToDictionaryAsync(c => c.Id, cancellationToken);

        var result = top
            .Select(x =>
            {
                names.TryGetValue(x.CategoryId, out var category);
                var name = category?.Name ?? category?.Code ?? $"#{x.CategoryId}";
                return new DashboardCategoryDto(x.CategoryId, name, x.Count);
            })
            .ToList();

        var otherCount = counts.Skip(TopCategories).Sum(x => x.Count);

        if (otherCount > 0)
            result.Add(new DashboardCategoryDto(null, OtherCategoriesName, otherCount));

        return result;
    }

    private async Task<List<DashboardGradeDto>> GetGradesAsync(
        CancellationToken cancellationToken)
    {
        var counts = await Reviews()
            .GroupBy(x => x.Grade)
            .Select(g => new { Grade = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Grade, x => x.Count, cancellationToken);

        return Enumerable.Range(1, 5)
            .Reverse()
            .Select(grade => new DashboardGradeDto(
                grade,
                counts.GetValueOrDefault(grade)))
            .ToList();
    }

    private async Task<List<DashboardUserDto>> GetLatestUsersAsync(
        CancellationToken cancellationToken)
    {
        return await Users()
            .OrderByDescending(x => x.RegisteredAt)
            .ThenByDescending(x => x.Id)
            .Take(ListSize)
            .Select(x => new DashboardUserDto(
                x.Id,
                x.UserName,
                x.Email,
                x.Phone,
                x.Avatar != null ? x.Avatar.Url : null,
                x.RegisteredAt))
            .ToListAsync(cancellationToken);
    }

    private async Task<List<DashboardAdvertisementDto>> GetLatestAdvertisementsAsync(
        CancellationToken cancellationToken)
    {
        return await Advertisements()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Take(ListSize)
            .Select(x => new DashboardAdvertisementDto(
                x.Id,
                x.Title,
                x.Pictures
                    .OrderBy(p => p.Order)
                    .Select(p => p.Url)
                    .FirstOrDefault(),
                x.Variants
                    .Where(v => v.EntityStatus != EntityStatus.Deleted)
                    .OrderBy(v => v.Id)
                    .Select(v => (decimal?)v.Price)
                    .FirstOrDefault() ?? x.Price,
                x.Currency.Symbol,
                x.SoldAt))
            .ToListAsync(cancellationToken);
    }

    private async Task<List<DashboardSellerDto>> GetTopSellersAsync(
        CancellationToken cancellationToken)
    {
        var counts = await Advertisements()
            .GroupBy(x => x.OwnerId)
            .Select(g => new { OwnerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(ListSize)
            .ToListAsync(cancellationToken);

        var ownerIds = counts.Select(x => x.OwnerId).ToList();

        var sellers = await _db.Users
            .Where(u => ownerIds.Contains(u.Id))
            .Select(u => new
            {
                u.Id,
                u.UserName,
                AvatarUrl = u.Avatar != null ? u.Avatar.Url : null
            })
            .ToDictionaryAsync(u => u.Id, cancellationToken);

        return counts
            .Where(x => sellers.ContainsKey(x.OwnerId))
            .Select(x =>
            {
                var seller = sellers[x.OwnerId];
                return new DashboardSellerDto(
                    seller.Id,
                    seller.UserName,
                    seller.AvatarUrl,
                    x.Count);
            })
            .ToList();
    }

    private async Task<List<DashboardLowStockDto>> GetLowStockAsync(
        CancellationToken cancellationToken)
    {
        var rows = await Advertisements()
            .Where(x => x.SoldAt == null)
            .Select(x => new
            {
                x.Id,
                x.Title,
                PictureUrl = x.Pictures
                    .OrderBy(p => p.Order)
                    .Select(p => p.Url)
                    .FirstOrDefault(),
                SellerUserName = x.Owner.UserName,
                Stock = x.Variants
                    .Where(v => v.EntityStatus != EntityStatus.Deleted)
                    .Sum(v => v.Stock)
            })
            .Where(x => x.Stock <= LowStockThreshold)
            .OrderBy(x => x.Stock)
            .ThenBy(x => x.Id)
            .Take(ListSize)
            .ToListAsync(cancellationToken);

        return rows
            .Select(x => new DashboardLowStockDto(
                x.Id,
                x.Title,
                x.PictureUrl,
                x.SellerUserName,
                x.Stock))
            .ToList();
    }

    private IQueryable<User> Users()
    {
        return _db.Users
            .AsNoTracking()
            .WithStatusFilter(EntityStatus.ActiveAndHidden);
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
            .WithStatusFilter(EntityStatus.ActiveAndHidden);
    }
}
