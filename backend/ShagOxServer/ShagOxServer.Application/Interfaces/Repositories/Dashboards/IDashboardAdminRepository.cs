using ShagOxServer.Application.DTOs.Dashboards;

namespace ShagOxServer.Application.Interfaces.Repositories.Dashboards;
public interface IDashboardAdminRepository
{
    Task<DashboardAdminDto> GetDashboardAsync(
        DashboardAdminQueryOptions options,
        string language,
        CancellationToken cancellationToken = default);
}
