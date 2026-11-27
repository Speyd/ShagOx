using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.UserDevices.Admin;

[ApiController]
[Route("api/admin/user-devices")]
[Authorize(Roles = "Admin")]
public class UserDeviceAdminCommandsController
    : ApiController
{
    private readonly IUserTrastManagementService _trastManagement;


    public UserDeviceAdminCommandsController(
        IUserTrastManagementService trastManagement,
        IUserContext userContext
    )
        : base(userContext)
    {
        _trastManagement = trastManagement;
    }


    [HttpPost("{id:long}/untrus")]
    public async Task<IActionResult> UntrustDevice(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _trastManagement
            .UntrustDeviceByAdminAsync(
                id,
                cancellationToken);

        return result.ToActionResult();
    }
}
