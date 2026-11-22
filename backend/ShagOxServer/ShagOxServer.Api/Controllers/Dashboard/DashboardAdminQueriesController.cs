using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dashboards;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dashboard;
[ApiController]
[Route("api/admin/dashboard")]

public class DashboardAdminQueriesController
    : ApiController
{
    private readonly IDashboardAdminService _dashboardService;


    public DashboardAdminQueriesController(
        IDashboardAdminService dashboardService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _dashboardService = dashboardService;
    }


    [HttpGet]
    public async Task<IActionResult> GetByOptions(
        [FromQuery] DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        var result = await _dashboardService
            .GetDashboardAsync(options, cancellationToken);

        return result.ToActionResult();
    }
}
