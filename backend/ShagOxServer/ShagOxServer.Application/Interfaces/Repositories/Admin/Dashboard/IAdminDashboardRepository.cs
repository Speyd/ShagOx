using ShagOxServer.Application.DTOs.Admin.Dashboard;

namespace ShagOxServer.Application.Interfaces.Repositories.Admin.Dashboard;
public interface IAdminDashboardRepository
{
    Task<AdminDashboardDto> GetDashboardAsync(
        string language,
        CancellationToken cancellationToken = default);
}
