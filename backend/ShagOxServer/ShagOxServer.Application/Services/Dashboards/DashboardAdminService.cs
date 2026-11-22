using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Application.Interfaces.Services.Dashboards;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dashboards;
public class DashboardAdminService
    : IDashboardAdminService
{
    private readonly IDashboardAdminRepository _repository;
    private readonly ILanguageProvider _language;


    public DashboardAdminService(
        IDashboardAdminRepository repository,
        ILanguageProvider language)
    {
        _repository = repository;
        _language = language;
    }


    public async Task<Result<DashboardAdminDto>> GetDashboardAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken = default)
    {
        var dashboard = await _repository
            .GetDashboardAsync(
                options,
                _language.Language, 
                cancellationToken);

        return Result<DashboardAdminDto>.Success(dashboard);
    }
}
