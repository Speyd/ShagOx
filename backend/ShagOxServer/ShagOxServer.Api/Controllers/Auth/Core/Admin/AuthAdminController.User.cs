using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;

namespace ShagOxServer.Api.Controllers.Auth.Core.Admin;
public partial class AuthAdminController
    : ApiCookieController
{
    [HttpPost("user/{userId:long}/ban")]
    public async Task<IActionResult> BanUser(
        [FromRoute] long userId,
        CancellationToken cancellationToken,
        [FromQuery] DateTime? blockedUntil = null)
    {
        await _userModerationService.BanUserAsync(
            userId,
            blockedUntil,
            cancellationToken);

        return Ok();
    }

    [HttpPost("user/{userId:long}/unban")]
    public async Task<IActionResult> UnbanUser(
        [FromRoute] long userId,
        CancellationToken cancellationToken)
    {
        await _userModerationService.UnbanUserAsync(
            userId,
            cancellationToken);

        return Ok();
    }
}
