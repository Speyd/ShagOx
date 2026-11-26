using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;

namespace ShagOxServer.Api.Controllers.Auth.Core.Admin;
public partial class AuthAdminController
    : ApiCookieController
{
    [HttpPost("user-device/{deviceId:long}/ban")]
    public async Task<IActionResult> BanUserDevice(
        [FromRoute] long deviceId,
        CancellationToken cancellationToken,
        [FromQuery] DateTime? blockedUntil = null)
    {
        await _userModerationService.BanUserDeviceAsync(
            deviceId,
            blockedUntil,
            cancellationToken);

        return Ok();
    }

    [HttpPost("user-device/{deviceId:long}/unban")]
    public async Task<IActionResult> UnbanUserDevice(
        [FromRoute] long deviceId,
        CancellationToken cancellationToken)
    {
        await _userModerationService.UnbanUserDeviceAsync(
            deviceId,
            cancellationToken);

        return Ok();
    }
}
