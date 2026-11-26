using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;

namespace ShagOxServer.Api.Controllers.Auth.Core.Admin;
public partial class AuthAdminController
    : ApiCookieController
{
    [HttpPost("device/{deviceId:long}/ban")]
    public async Task<IActionResult> BanDevice(
        [FromRoute] long deviceId,
        CancellationToken cancellationToken,
        [FromQuery] DateTime? blockedUntil = null)
    {
        await _deviceModerationService.BanDeviceAsync(
            deviceId,
            blockedUntil,
            cancellationToken);

        return Ok();
    }

    [HttpPost("device/{deviceId:long}/unban")]
    public async Task<IActionResult> UnbanDevice(
        [FromRoute] long deviceId,
        CancellationToken cancellationToken)
    {
        await _deviceModerationService.UnbanDeviceAsync(
            deviceId,
            cancellationToken);

        return Ok();
    }
}
