using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dashboards;
public interface IDashboardAdminService
{
    Task<Result<DashboardAdminDto>> GetDashboardAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken = default);
}
