using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Admin.Dashboard;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Admin.Dashboard;

[ApiController]
[Route("api/admin/dashboard")]
[Authorize(Roles = "Admin")]
public class AdminDashboardController
    : ApiController
{
    private readonly IAdminDashboardService _dashboardService;


    public AdminDashboardController(
        IAdminDashboardService dashboardService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _dashboardService = dashboardService;
    }


    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var result = await _dashboardService
            .GetDashboardAsync(cancellationToken);

        return result.ToActionResult();
    }
}
