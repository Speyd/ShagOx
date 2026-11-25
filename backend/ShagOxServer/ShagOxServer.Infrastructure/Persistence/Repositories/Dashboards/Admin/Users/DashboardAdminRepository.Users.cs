using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.DTOs.Dashboards.Users;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Users.Dtos;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;
public partial class DashboardAdminRepository
    : IDashboardAdminRepository
{
    private async Task<List<DashboardUserDto>> GetLatestUsersAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        return await Users()
            .OrderByDescending(x => x.RegisteredAt)
            .ThenByDescending(x => x.Id)
            .Take(options.MaxItemsPerWidget)
            .ToDashboardUserDto()
            .ToListAsync(cancellationToken);
    }

    private async Task<List<DashboardSellerDto>> GetTopSellersAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        var sellerCounts = await GetTopSellerCountsAsync(
            options,
            cancellationToken);

        var sellers = await GetSellersAsync(
            sellerCounts.Select(x => x.OwnerId),
            cancellationToken);

        return MapToSellerDtos(sellerCounts, sellers);
    }

    private async Task<List<SellerCount>> GetTopSellerCountsAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        var counts = await Advertisements()
           .GroupBy(x => x.OwnerId)
           .Select(g => new
           {
               OwnerId = g.Key,
               Count = g.Count()
           })
           .OrderByDescending(x => x.Count)
           .Take(options.MaxItemsPerWidget)
           .ToListAsync(cancellationToken);

        return counts
            .Select(x => new SellerCount(x.OwnerId, x.Count))
            .ToList();
    }

    private async Task<Dictionary<long, SellerInfo>> GetSellersAsync(
        IEnumerable<long> ownerIds,
        CancellationToken cancellationToken)
    {
        var ids = ownerIds.ToList();

        return await _db.Users
            .Where(u => ids.Contains(u.Id))
            .Select(u => new SellerInfo(
                u.Id,
                u.UserName,
                u.Avatar != null ? u.Avatar.Url : null))
            .ToDictionaryAsync(u => u.Id, cancellationToken);
    }

    private static List<DashboardSellerDto> MapToSellerDtos(
        IEnumerable<SellerCount> counts,
        Dictionary<long, SellerInfo> sellers)
    {
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
}
