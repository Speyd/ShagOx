using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.DTOs.Dashboards.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;
public partial class DashboardAdminRepository
    : IDashboardAdminRepository
{
    private async Task<List<DashboardAdvertisementDto>> GetLatestAdvertisementsAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        return await Advertisements()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Take(options.MaxItemsPerWidget)
            .ToDashboardAdvertisementDto()
            .ToListAsync(cancellationToken);
    }

    private async Task<List<DashboardLowStockDto>> GetLowStockAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        return await Advertisements()
           .Where(x => x.SoldAt == null)
           .Select(x => new
           {
               Advertisement = x,
               Stock = x.Variants
                   .Where(v => v.EntityStatus != EntityStatus.Deleted)
                   .Sum(v => v.Advertisement.Stock ?? v.Stock)
           })
           .Where(x => x.Stock <= options.LowStockThreshold)
           .OrderBy(x => x.Stock)
           .ThenBy(x => x.Advertisement.Id)
           .Take(options.MaxItemsPerWidget)
           .Select(x => new DashboardLowStockDto(
               x.Advertisement.Id,
               x.Advertisement.Title,
               x.Advertisement.Pictures
                   .OrderBy(p => p.Order)
                   .Select(p => p.Url)
                   .FirstOrDefault(),
               x.Advertisement.Owner.UserName,
               x.Stock))
           .ToListAsync(cancellationToken);
    }
}
