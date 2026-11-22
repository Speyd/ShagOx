using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Comments.Review;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;
public partial class DashboardAdminRepository
    : IDashboardAdminRepository
{
    private readonly ReplicaDbContext _db;


    public DashboardAdminRepository(
        ReplicaDbContext db)
    {
        _db = db;
    }


    public async Task<DashboardAdminDto> GetDashboardAsync(
        DashboardAdminQueryOptions options,
        string language,
        CancellationToken cancellationToken = default)
    {
        var totals = await GetTotalsAsync(cancellationToken);

        var growth = await GetGrowthAsync(
            options, 
            cancellationToken);

        var byCategory = await GetAdvertisementsByCategoryAsync(
            options,
            language, 
            cancellationToken);

        var grades = await GetGradesAsync(
            cancellationToken);

        var latestUsers = await GetLatestUsersAsync(
            options, 
            cancellationToken);

        var latestAdvertisements = await GetLatestAdvertisementsAsync(
            options, 
            cancellationToken);

        var topSellers = await GetTopSellersAsync(
            options, 
            cancellationToken);

        var lowStock = await GetLowStockAsync(
            options, 
            cancellationToken);

        return new DashboardAdminDto(
            totals,
            growth,
            byCategory,
            grades,
            latestUsers,
            latestAdvertisements,
            topSellers,
            lowStock);
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
