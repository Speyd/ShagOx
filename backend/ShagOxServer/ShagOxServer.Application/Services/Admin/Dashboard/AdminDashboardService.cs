using ShagOxServer.Application.DTOs.Admin.Dashboard;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Admin.Dashboard;
using ShagOxServer.Application.Interfaces.Services.Admin.Dashboard;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Admin.Dashboard;
public class AdminDashboardService
    : IAdminDashboardService
{
    private readonly IAdminDashboardRepository _repository;
    private readonly ILanguageProvider _language;


    public AdminDashboardService(
        IAdminDashboardRepository repository,
        ILanguageProvider language)
    {
        _repository = repository;
        _language = language;
    }


    public async Task<Result<AdminDashboardDto>> GetDashboardAsync(
        CancellationToken cancellationToken = default)
    {
        var dashboard = await _repository
            .GetDashboardAsync(_language.Language, cancellationToken);

        return Result<AdminDashboardDto>.Success(dashboard);
    }
}
