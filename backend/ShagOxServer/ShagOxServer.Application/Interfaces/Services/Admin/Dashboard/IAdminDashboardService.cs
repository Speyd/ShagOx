using ShagOxServer.Application.DTOs.Admin.Dashboard;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Admin.Dashboard;
public interface IAdminDashboardService
{
    Task<Result<AdminDashboardDto>> GetDashboardAsync(
        CancellationToken cancellationToken = default);
}
