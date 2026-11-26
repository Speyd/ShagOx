using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;

namespace ShagOxServer.Api.Controllers.Auth.Core.Admin;

public partial class AuthAdminController
    : ApiCookieController
{
    [HttpPost("{userId:long}/logout-all")]
    public async Task<IActionResult> LogoutAll(
        [FromRoute] long userId,
        CancellationToken cancellationToken)
    {
        await _logoutService.LogoutAllAsync(
            userId,
            cancellationToken);

        DeleteTokens(Response);

        return Ok();
    }

    [HttpPost("{userId:long}/logout-other")]
    public async Task<IActionResult> LogoutOther(
        [FromRoute] long userId,
        CancellationToken cancellationToken)
    {
        await _logoutService.LogoutOtherSessions(
            userId,
            cancellationToken);

        DeleteTokens(Response);

        return Ok();
    }
}
