using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.UserDevices;

[ApiController]
[Route("api/user-devices")]
[Authorize]
public class UserDeviceCommandsController
    : ApiController
{
    private readonly IUserTrastManagementService _trastManagement;


    public UserDeviceCommandsController(
        IUserTrastManagementService trastManagement,
        IUserContext userContext
    )
        : base(userContext)
    {
        _trastManagement = trastManagement;
    }


    [HttpPost("{id:long}/untrust")]
    public async Task<IActionResult> UntrustDevice(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _trastManagement.UntrustDeviceAsync(
            id,
            cancellationToken);

        return result.ToActionResult();
    }
}
