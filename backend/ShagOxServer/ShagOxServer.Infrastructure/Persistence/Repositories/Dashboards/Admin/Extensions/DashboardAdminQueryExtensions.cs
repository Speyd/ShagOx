using ShagOxServer.Application.DTOs.Dashboards.Advertisements;
using ShagOxServer.Application.DTOs.Dashboards.Users;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Extensions;
public static class DashboardAdminQueryExtensions
{
    public static IQueryable<DashboardAdvertisementDto> ToDashboardAdvertisementDto(
        this IQueryable<Advertisement> x,
        EntityStatus status = EntityStatus.Active)
    {
        return x.Select(x => new DashboardAdvertisementDto(
            x.Id,
            x.Title,
            x.Pictures
                .OrderBy(p => p.Order)
                .Select(p => p.Url)
                .FirstOrDefault(),
            x.Variants
                .Where(v => v.EntityStatus != EntityStatus.Deleted)
                .OrderBy(v => v.Id)
                .Select(v => v.Advertisement.Price ?? (decimal?)v.Price)
                .FirstOrDefault() ?? x.Price,
            x.Currency.Symbol,
            x.SoldAt));
    }
    
    public static IQueryable<DashboardUserDto> ToDashboardUserDto(
        this IQueryable<User> x,
        EntityStatus status = EntityStatus.Active)
    {
        return x.Select(x => new DashboardUserDto(
            x.Id,
            x.UserName,
            x.Email,
            x.Phone,
            x.Avatar != null ? x.Avatar.Url : null,
            x.RegisteredAt));
    }
}
