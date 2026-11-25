using ShagOxServer.Application.DTOs.Dashboards.Advertisements;
using ShagOxServer.Application.DTOs.Dashboards.Analytics;
using ShagOxServer.Application.DTOs.Dashboards.Growth;
using ShagOxServer.Application.DTOs.Dashboards.Statistics;
using ShagOxServer.Application.DTOs.Dashboards.Users;

namespace ShagOxServer.Application.DTOs.Dashboards;
public sealed record DashboardAdminDto
(
    DashboardTotalsDto Totals,
    List<DashboardGrowthDto> Growth,
    List<DashboardCategoryDto> AdvertisementsByCategory,
    List<DashboardGradeDto> Grades,
    List<DashboardUserDto> LatestUsers,
    List<DashboardAdvertisementDto> LatestAdvertisements,
    List<DashboardSellerDto> TopSellers,
    List<DashboardLowStockDto> LowStock
);
