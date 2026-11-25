using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;

namespace ShagOxServer.Api.Controllers.Auth.Core;
public partial class AuthController
    : ApiCookieController
{
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(
        CancellationToken cancellationToken)
    {
        var refreshToken = UserContext.RefreshToken;

        if (refreshToken is not null)
        {
            await _logoutService.LogoutAsync(
                refreshToken,
                cancellationToken);
        }

        DeleteTokens(Response);

        return Ok(new { message = "Logged out successfully" });
    }

    [HttpPost("logout-all")]
    [Authorize]
    public async Task<IActionResult> LogoutAll(
    CancellationToken cancellationToken)
    {
        var userId = UserContext.UserId;

        if (userId is null)
            return Unauthorized();

        await _logoutService.LogoutAllAsync(
            userId.Value,
            cancellationToken);

        DeleteTokens(Response);

        return Ok(new { message = "Logged out from all devices successfully" });
    }

    [HttpPost("logout-other")]
    [Authorize]
    public async Task<IActionResult> LogoutOther(
    CancellationToken cancellationToken)
    {
        var userId = UserContext.UserId;

        if (userId is null)
            return Unauthorized();

        await _logoutService.LogoutOtherSessions(
            userId.Value,
            cancellationToken);

        DeleteTokens(Response);

        return Ok(new { message = "Logged out from other devices successfully" });
    }
}
